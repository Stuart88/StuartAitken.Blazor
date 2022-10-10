using Microsoft.EntityFrameworkCore;
using StuartAitken.Blazor.Server.DataAccess;
using StuartAitken.Blazor.Server.DataAccess.Entities;
using StuartAitken.Blazor.Server.Mapper;
using StuartAitken.Blazor.Shared.Models;

namespace StuartAitken.Blazor.Server.DataService
{
    public class ProjectsService : ServiceBase
    {
        public ProjectsService() { }

        #region Porfolio Projects

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
                    .Where(p => p.ID == id)
                    .Select(p => Mapper.Mapper.Map<PortfolioProject, Project>(p))
                    .FirstOrDefaultAsync();
            }
            catch
            {
                throw;
            }
        }

        public Task<Project?> GetPortfolioProject(PortfolioProject project)
        {
            try
            {
                int justAddedID =
                    _db.PortfolioProjects
                        .Where(
                            x => x.CreationDate == project.CreationDate && x.Name == project.Name
                        )
                        .FirstOrDefault()
                        ?.ID ?? 0;

                return GetPortfolioProject(justAddedID);
            }
            catch
            {
                throw;
            }
        }

        public async Task<int> AddPortfolioProject(Project project)
        {
            try
            {
                var portfolioProject = Mapper.Mapper.Map<Project, PortfolioProject>(project);
                portfolioProject.Urls = portfolioProject.Urls ?? "";
                _db.Add(portfolioProject);
                return await _db.SaveChangesAsync();
            }
            catch
            {
                throw;
            }
        }

        public async Task<int> UpdatePortfolioProject(Project project)
        {
            try
            {
                PortfolioProject updating = _db.PortfolioProjects.Find(project.ID);

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
                return await _db.SaveChangesAsync();
            }
            catch (Exception e)
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

        public bool PortfolioProjectExists(int ID)
        {
            return GetAllPortfolioProjects().Any(e => e.ID == ID);
        }

        #endregion
    }
}
