using Microsoft.EntityFrameworkCore;
using StuartAitken.Blazor.Server.DataAccess.Entities;
using StuartAitken.Blazor.Shared.Models;

namespace StuartAitken.Blazor.Server.DataService
{
    public class ProjectsService : ServiceBase
    {
        #region Public Constructors

        public ProjectsService() { }

        #endregion Public Constructors

        #region Public Methods

        public async Task<Project> AddPortfolioProject(Project project)
        {
            try
            {
                var portfolioProject = Mapper.Mapper.Map<Project, PortfolioProject>(project);
                portfolioProject.Urls = portfolioProject.Urls ?? "";
                _db.Add(portfolioProject);
                await _db.SaveChangesAsync();
                return Mapper.Mapper.Map<PortfolioProject, Project>(portfolioProject);
            }
            catch
            {
                throw;
            }
        }

        public async Task<int> DeletePortfolioProjectAsync(int id)
        {
            try
            {
                PortfolioProject portfolioProject = _db.PortfolioProjects.Find(id);
                _db.PortfolioProjects.Remove(portfolioProject);
                _db.Entry(portfolioProject).State = EntityState.Deleted;
                return await _db.SaveChangesAsync();
            }
            catch
            {
                throw;
            }
        }

        public IEnumerable<Project> GetAllPortfolioProjects()
        {
            try
            {
                IEnumerable<Project> allProjects = _db.PortfolioProjects
                    .OrderByDescending(x => x.ProjectDate)
                    .Select(p => Mapper.Mapper.Map<PortfolioProject, Project>(p))
                    .ToList();

                foreach (Project p in allProjects)
                {
                    if (p.Description != null && p.Description.Length > 200)
                        p.Description = p.Description.Substring(0, 200) + "...";
                }
                return allProjects;
            }
            catch
            {
                throw;
            }
        }

        public Task<Project?> GetPortfolioProject(int id)
        {
            try
            {
                return _db.PortfolioProjects
                    .AsNoTracking()
                    .Where(p => p.ID == id)
                    .Select(p => Mapper.Mapper.Map<PortfolioProject, Project>(p))
                    .FirstOrDefaultAsync();
            }
            catch
            {
                throw;
            }
        }

        public Task<List<ProjectTech>> GetProjectTechs()
        {
            try
            {
                return _db.PortfolioProjectTeches
                    .Select(p => Mapper.Mapper.Map<PortfolioProjectTech, ProjectTech>(p))
                    .ToListAsync();
            }
            catch
            {
                throw;
            }
        }

        public Task<List<ProjectType>> GetProjectTypes()
        {
            try
            {
                return _db.PortfolioProjectTypes
                    .Select(p => Mapper.Mapper.Map<PortfolioProjectType, ProjectType>(p))
                    .ToListAsync();
            }
            catch
            {
                throw;
            }
        }

        public async Task IncrementViews(int projectId)
        {
            try
            {
                var project = await _db.PortfolioProjects
                    .Where(p => p.ID == projectId)
                    .FirstOrDefaultAsync();

                if (project != null)
                {
                    project.Views ??= 0;
                    project.Views += 1;
                    _db.Update(project);
                    await _db.SaveChangesAsync();
                }
            }
            catch
            {
                throw;
            }
        }

        public bool PortfolioProjectExists(int ID)
        {
            return GetAllPortfolioProjects().Any(e => e.ID == ID);
        }

        public async Task<Project?> UpdatePortfolioProject(Project project)
        {
            try
            {
                if (project == null)
                    throw new Exception("Project was null");

                PortfolioProject? updating = _db.PortfolioProjects.Find(project.ID);

                if (updating == null)
                    throw new Exception("Project not found!");

                updating.Name = project.Name;
                updating.ProjectDate = project.ProjectDate;
                updating.ProjectDurationDays = project.ProjectDurationDays;
                updating.Status = project.Status;
                updating.Tech = project.Tech;
                updating.Description = project.Description;
                updating.Urls = project.Urls ?? "";
                updating.Type = project.Type;
                updating.ModifiedDate = DateTime.Now;

                _db.Entry(updating).State = EntityState.Modified;
                await _db.SaveChangesAsync();

                return await GetPortfolioProject(project.ID);
            }
            catch (Exception e)
            {
                throw;
            }
        }

        #endregion Public Methods
    }
}
