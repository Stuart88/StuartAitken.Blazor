using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using StuartAitken.Blazor.Server.DataService;
using StuartAitken.Blazor.Server.Helpers;
using StuartAitken.Blazor.Shared.Models;

namespace StuartAitken.Blazor.Server.Controllers
{
    [Route("api/projects/")]
    [ApiController]
    public class ProjectsController : ControllerBase
    {
        #region Private Fields

        private ProjectImageService _projectImageService;
        private ProjectsService _projectsService;

        #endregion Private Fields

        #region Public Constructors

        public ProjectsController(
            ProjectsService projectsService,
            ProjectImageService projectImageService
        )
        {
            this._projectsService = projectsService;
            this._projectImageService = projectImageService;
        }

        #endregion Public Constructors

        #region Public Methods

        [HttpPost]
        public async Task<ApiResponse<Project>> AddPortfolioProject(
            [FromBody] Project portfolioProject
        )
        {
            portfolioProject.CreationDate = DateTime.Now;
            portfolioProject.ModifiedDate = DateTime.Now;

            Project? newlyAdded = null;

            try
            {
                int projectId = await _projectsService.AddPortfolioProjectAndGetID(portfolioProject);

                //if (Request.Form.Files.Count > 0)
                //{
                //    //if form data has anything else, it's an image! So add it.
                //    newlyAdded = await _projectsService.GetPortfolioProject(
                //        projectId
                //    );

                //    if (newlyAdded == null)
                //        throw new Exception("Could not find just-added project?!");

                //    //foreach (IFormFile file in Request.Form.Files)
                //    //{
                //    //    await _projectImageService.AddNewProjectImageAsync(
                //    //        ByteHelper.ConvertToBytes(file),
                //    //        newlyAdded.ID
                //    //    );
                //    //}
                //}
            }
            catch (DbUpdateException)
            {
                if (_projectsService.PortfolioProjectExists(portfolioProject.ID))
                {
                    return new ApiResponse<Project>("DB conflict; Project ID exists");
                }
                else
                {
                    throw;
                }
            }

            if (newlyAdded == null)
                return new ApiResponse<Project>("newlyAdded is null?!");

            return new ApiResponse<Project>(newlyAdded);
        }

        [HttpDelete("{id}")]
        public async Task<ApiResponse> DeletePortfolioProject([FromRoute] int id)
        {
            await _projectImageService.DeleteAllImagesForProjectId(id);

            await _projectsService.DeletePortfolioProjectAsync(id);

            return new ApiResponse();
        }

        [HttpPut("{id}")]
        public async Task<ApiResponse<Project>> EditPortfolioProject(
            [FromRoute] int ID,
            [FromBody] Project portfolioProject
        )
        {
            if (ID != portfolioProject.ID)
            {
                return new ApiResponse<Project>("Portfolio ID mismatch.");
            }
            try
            {
                var updatedProject = await _projectsService.UpdatePortfolioProject(portfolioProject);
                if (updatedProject != null)
                {
                    ////check for image in Form (will always be second file in form)
                    //if (Request.Form.Files.Count > 0)
                    //{
                    //    foreach (IFormFile file in Request.Form.Files)
                    //    {
                    //        await _projectImageService.AddNewProjectImageAsync(
                    //            ByteHelper.ConvertToBytes(file),
                    //            ID
                    //        );
                    //    }
                    //}

                    return new ApiResponse<Project>(updatedProject);
                }
                else
                {
                    return new ApiResponse<Project>("Update error. Nothing updated.");
                }
            }
            catch (Exception ex)
            {
                return new ApiResponse<Project>(ex.Message);
            }
        }

        [HttpGet("all")]
        public IEnumerable<Project> GetAllPortfolioProjects()
        {
            var projects = _projectsService.GetAllPortfolioProjects();

            return projects;
        }

        [HttpGet("{id}")]
        public async Task<Project> GetPortfolioProject([FromRoute] int id)
        {
            if (id == 0)
                return new Project();

            var portfolioProject = await _projectsService.GetPortfolioProject(id);

            if (portfolioProject == null)
            {
                return null;
            }

            portfolioProject.Views = portfolioProject.Views ?? 0;
            portfolioProject.Images = _projectImageService.GetProjectImages(id);

            return portfolioProject;
        }

        /// <summary>
        /// Gets list of portfolio item names and IDs, for populating dropdown list in navbar
        /// </summary>
        /// <returns></returns>
        [HttpGet("select-list")]
        public IEnumerable<SelectListItem<int>> GetPortfolioProjectListItems()
        {
            IEnumerable<SelectListItem<int>> items = _projectsService
                .GetAllPortfolioProjects()
                .Select(
                    x =>
                        new SelectListItem<int>
                        {
                            Value = x.ID,
                            Label = x.Name,
                            GroupName = x.Type
                        }
                );

            return items;
        }

        [HttpGet("techs")]
        public async Task<List<ProjectTech>> GetProjectTechs()
        {
            var techs = await _projectsService.GetProjectTechs();

            return techs;
        }

        [HttpGet("types")]
        public async Task<List<ProjectType>> GetProjectTypes()
        {
            var types = await _projectsService.GetProjectTypes();

            return types;
        }

        [HttpPost("viewedProject")]
        public async Task<ApiResponse> ViewedProject([FromBody] int id)
        {
            var portfolioProject = await _projectsService.GetPortfolioProject(id);

            if (portfolioProject == null)
            {
                return new ApiResponse("Project not found");
            }

            portfolioProject.Views = portfolioProject.Views ?? 0;
            portfolioProject.Views++;

            _ = await _projectsService.UpdatePortfolioProject(portfolioProject);

            return new ApiResponse();
        }

        #endregion Public Methods
    }
}