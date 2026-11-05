using Microsoft.AspNetCore.Mvc;
using StuartAitken.Blazor.Server.ActionFilters;
using StuartAitken.Blazor.Server.DataService;
using StuartAitken.Blazor.Server.Exceptions;
using StuartAitken.Blazor.Shared.Models;

namespace StuartAitken.Blazor.Server.Controllers
{
    [Route("api/project-images")]
    [ApiController]
    public class ProjectImageController : CustomControllerBase
    {
        #region Private Fields

        private readonly ILogger<ProjectImageController> _logger;
        private readonly IWebHostEnvironment _webHostEnvironment;
        private ProjectImageService _projectImageService;
        private ProjectsService _projectService;

        #endregion Private Fields

        #region Private Properties

        private string imageRootFolder =>
            Path.Combine(_webHostEnvironment.WebRootPath, "images", "projectImages");

        #endregion Private Properties

        #region Public Constructors

        public ProjectImageController(
            ProjectImageService projectImageService,
            ProjectsService projectService,
            IWebHostEnvironment webHostEnvironment,
            ILogger<ProjectImageController> logger
        )
        {
            this._projectService = projectService;
            this._projectImageService = projectImageService;
            this._webHostEnvironment = webHostEnvironment;
            this._logger = logger;
        }

        #endregion Public Constructors

        #region Public Methods

        [AdminAuthorise]
        [HttpDelete("{id}")]
        public async Task<ApiResponse> DeletePortfolioImage([FromRoute] int id)
        {
            try
            {
                var image = await _projectImageService.GetPortfolioProjectImageAsync(id);

                if (image == null)
                {
                    return new ApiResponse(new Exception("Image not found"));
                }

                await _projectImageService.DeletePortfolioProjectImage(imageRootFolder, id);

                return new ApiResponse();
            }
            catch (UserFacingException e)
            {
                return new ApiResponse(ok: false) { Error = e.Message };
            }
            catch (Exception e)
            {
                _logger.LogError(e, "Failed to delete image {ImageId}", id);
                return new ApiResponse(ok: false) { Error = "An unexpected error occurred." };
            }
        }

        [HttpGet("{id}")]
        public async Task<ProjectImage> GetPortfolioImage([FromRoute] int id)
        {
            var image = await _projectImageService.GetPortfolioProjectImageAsync(id);

            return image;
        }

        [HttpGet("image-object/{id}")]
        public async Task<IActionResult> GetPortfolioImageObject([FromRoute] int id)
        {
            string imagePath = Path.Combine(imageRootFolder, $"{id}.png");

            return PhysicalFile(imagePath, "image/png");
        }

        [HttpGet("image-ids-for-project/{projID}")]
        public IEnumerable<int> GetProjectImageIDs([FromRoute] int projID)
        {
            var projectImageIDs = _projectImageService.GetProjectImageIDs(projID);

            return projectImageIDs;
        }

        [HttpGet("images-for-project/{projID}")]
        public IEnumerable<ProjectImage> GetProjectImages([FromRoute] int projID)
        {
            var projectImages = _projectImageService.GetProjectImages(projID);

            return projectImages;
        }

        [AdminAuthorise]
        [HttpPost("{projectId}")]
        public async Task<ApiResponse<List<ProjectImage>>> PostProjectImage(
            [FromForm] IEnumerable<IFormFile> images,
            [FromRoute] int projectId
        )
        {
            var project = await _projectService.GetPortfolioProject(projectId);

            ApiResponse<List<ProjectImage>> response = new();
            List<ProjectImage> addedImages = new();
            string errorMessage = "";

            if (project == null)
                return new ApiResponse<List<ProjectImage>>(new Exception("Project not found!"));

            foreach (var file in images)
            {
                try
                {
                    var addedImage = await _projectImageService.AddPortfolioProjectImageAsync(
                        imageRootFolder,
                        file,
                        projectId
                    );
                    if (addedImage != null)
                        addedImages.Add(addedImage);
                }
                catch (UserFacingException e)
                {
                    errorMessage += $"{e.Message}\n";
                    response.Ok = false;
                }
                catch (Exception e)
                {
                    _logger.LogError(e, "Failed to add image to project {ProjectId}", projectId);
                    errorMessage += "Failed to upload one or more images.\n";
                    response.Ok = false;
                }
            }

            response.Data = addedImages;
            response.Error = errorMessage;

            return response;
        }

        [AdminAuthorise]
        [HttpPut("reorder")]
        public async Task<ApiResponse> ReorderImages([FromBody] List<int> orderedImageIds)
        {
            try
            {
                await _projectImageService.ReorderProjectImages(orderedImageIds);

                return new ApiResponse();
            }
            catch (UserFacingException e)
            {
                return new ApiResponse(ok: false) { Error = e.Message };
            }
            catch (Exception e)
            {
                _logger.LogError(e, "Failed to reorder images");
                return new ApiResponse(ok: false) { Error = "An unexpected error occurred." };
            }
        }

        [AdminAuthorise]
        [HttpPost("set-main-image/{id}")]
        public async Task<ApiResponse> SetMainImage([FromRoute] int id)
        {
            try
            {
                return await _projectImageService.SetMainPortfolioProjectImageAsync(id) != 0
                    ? new ApiResponse()
                    : throw new UserFacingException("No data saved!");
            }
            catch (UserFacingException e)
            {
                return new ApiResponse(ok: false) { Error = e.Message };
            }
            catch (Exception e)
            {
                _logger.LogError(e, "Failed to set main image {ImageId}", id);
                return new ApiResponse(ok: false) { Error = "An unexpected error occurred." };
            }
        }

        #endregion Public Methods
    }
}
