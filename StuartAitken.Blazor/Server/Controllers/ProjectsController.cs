using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using StuartAitken.Blazor.Server.ActionFilters;
using StuartAitken.Blazor.Server.DataService;
using StuartAitken.Blazor.Server.Exceptions;
using StuartAitken.Blazor.Shared.Models;

namespace StuartAitken.Blazor.Server.Controllers
{
    [Route("api/projects/")]
    [ApiController]
    public class ProjectsController : CustomControllerBase
    {
        #region Private Fields

        private readonly ILogger<ProjectsController> _logger;
        private readonly IWebHostEnvironment _webHostEnvironment;
        private ProjectImageService _projectImageService;
        private ProjectsService _projectsService;

        #endregion Private Fields

        #region Private Properties

        private string imageRootFolder =>
            Path.Combine(_webHostEnvironment.WebRootPath, "images", "projectImages");

        #endregion Private Properties

        #region Public Constructors

        public ProjectsController(
            ProjectsService projectsService,
            ProjectImageService projectImageService,
            IWebHostEnvironment webHostEnvironment,
            ILogger<ProjectsController> logger
        )
        {
            this._projectsService = projectsService;
            this._projectImageService = projectImageService;
            this._webHostEnvironment = webHostEnvironment;
            this._logger = logger;
        }

        #endregion Public Constructors

        #region Public Methods

        [AdminAuthorise]
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
                newlyAdded = await _projectsService.AddPortfolioProject(portfolioProject);
            }
            catch (DbUpdateException)
            {
                if (_projectsService.PortfolioProjectExists(portfolioProject.ID))
                {
                    return new ApiResponse<Project>(new Exception("DB conflict; Project ID exists"));
                }
                else
                {
                    throw;
                }
            }

            if (newlyAdded == null)
                return new ApiResponse<Project>(new Exception("newlyAdded is null?!"));

            return new ApiResponse<Project>(newlyAdded);
        }

        [AdminAuthorise]
        [HttpDelete("{id}")]
        public async Task<ApiResponse> DeletePortfolioProject([FromRoute] int id)
        {
            try
            {
                await _projectImageService.DeleteAllImagesForProjectId(imageRootFolder, id);

                await _projectsService.DeletePortfolioProjectAsync(id);

                return new ApiResponse();
            }
            catch (UserFacingException e)
            {
                return new ApiResponse(ok: false) { Error = e.Message };
            }
            catch (Exception e)
            {
                _logger.LogError(e, "Failed to delete project {ProjectId}", id);
                return new ApiResponse(ok: false) { Error = "An unexpected error occurred." };
            }
        }

        [AdminAuthorise]
        [HttpPut("{id}")]
        public async Task<ApiResponse<Project>> EditPortfolioProject(
            [FromRoute] int ID,
            [FromBody] Project portfolioProject
        )
        {
            if (ID != portfolioProject.ID)
            {
                return new ApiResponse<Project>(new Exception("Portfolio ID mismatch."));
            }
            try
            {
                var updatedProject = await _projectsService.UpdatePortfolioProject(
                    portfolioProject
                );
                if (updatedProject != null)
                {
                    return new ApiResponse<Project>(updatedProject);
                }
                else
                {
                    return new ApiResponse<Project>(new Exception("Update error. Nothing updated."));
                }
            }
            catch (UserFacingException e)
            {
                return new ApiResponse<Project>(ok: false) { Error = e.Message };
            }
            catch (Exception e)
            {
                _logger.LogError(e, "Failed to update project {ProjectId}", ID);
                return new ApiResponse<Project>(ok: false) { Error = "An unexpected error occurred." };
            }
        }

        [HttpGet("all")]
        public IEnumerable<Project> GetAllPortfolioProjects()
        {
            var projects = _projectsService.GetAllPortfolioProjects();

            return projects;
        }

        [HttpGet("{id}")]
        public async Task<ApiResponse<Project>> GetPortfolioProject([FromRoute] int id)
        {
            try
            {
                var portfolioProject = await _projectsService.GetPortfolioProject(id);

                if (portfolioProject == null)
                {
                    throw new UserFacingException($"Project with ID {id} does not exist");
                }

                portfolioProject.Views ??= 0;
                portfolioProject.Images = _projectImageService.GetProjectImages(id);

                await _projectsService.IncrementViews(id);

                return new ApiResponse<Project>(portfolioProject);
            }
            catch (UserFacingException e)
            {
                return new ApiResponse<Project>(ok: false) { Error = e.Message };
            }
            catch (Exception e)
            {
                _logger.LogError(e, "Failed to get project {ProjectId}", id);
                return new ApiResponse<Project>(ok: false) { Error = "An unexpected error occurred." };
            }
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

        #endregion Public Methods
    }
}
