using Microsoft.AspNetCore.Mvc;
using StuartAitken.Blazor.Server.DataService;
using StuartAitken.Blazor.Shared.Models;

namespace StuartAitken.Blazor.Server.Controllers
{
    [Route("api/project-images")]
    [ApiController]
    public class ProjectImageController : ControllerBase
    {
        #region Private Fields

        private readonly IWebHostEnvironment _webHostEnvironment;
        private ProjectImageService _projectImageService;

        #endregion Private Fields

        #region Public Constructors

        public ProjectImageController(
            ProjectImageService projectImageService,
            IWebHostEnvironment webHostEnvironment
        )
        {
            this._projectImageService = projectImageService;
            this._webHostEnvironment = webHostEnvironment;
        }

        #endregion Public Constructors

        #region Public Methods

        // GET
        [HttpGet("{id}")]
        public async Task<ProjectImage> GetPortfolioImage([FromRoute] int id)
        {
            var image = await _projectImageService.GetPortfolioProjectImageAsync(id);

            return image;
        }

        [HttpGet("image-object/{id}")]
        public async Task<IActionResult> GetPortfolioImageObject([FromRoute] int id)
        {
            string imagePath = Path.Combine(
                _webHostEnvironment.WebRootPath,
                "images",
                "projectImages",
                $"{id}.png"
            );

            return PhysicalFile(imagePath, "image/png");
        }

        // GET (images for project)
        [HttpGet("image-ids-for-project/{projID}")]
        public IEnumerable<int> GetProjectImageIDs([FromRoute] int projID)
        {
            var projectImageIDs = _projectImageService.GetProjectImageIDs(projID);

            return projectImageIDs;
        }

        // GET (images for project)
        [HttpGet("images-for-project/{projID}")]
        public IEnumerable<ProjectImage> GetProjectImages([FromRoute] int projID)
        {
            var projectImages = _projectImageService.GetProjectImages(projID);

            return projectImages;
        }

        #endregion Public Methods

        //[HttpPut("{id}")]
        //public async Task<IActionResult> EditPortfolioImage([FromRoute] int id, [FromForm] PortfolioProjectImage image)
        //{
        //    if (!ModelState.IsValid)
        //    {
        //        return BadRequest(ModelState);
        //    }

        //    if (id != image.ID)
        //    {
        //        return BadRequest();
        //    }
        //    try
        //    {
        //        if (await _projectImageService.UpdatePortfolioProjectImageAsync(image) != 0)
        //        {
        //            return CreatedAtAction("EditPortfolioImage", new { id = image.ID }, image);
        //        }
        //        else
        //        {
        //            return StatusCode(500);
        //        }
        //    }
        //    catch
        //    {
        //        return StatusCode(500);
        //    }

        //}

        //[HttpPost("{id}")]
        //public async Task<IActionResult> SetMainImage([FromRoute] int id)
        //{
        //    try
        //    {
        //        return await _projectImageService.SetMainPortfolioProjectImageAsync(id) != 0
        //            ? Ok()
        //            : StatusCode(500);
        //    }
        //    catch
        //    {
        //        return StatusCode(500);
        //    }

        //}

        //[HttpPost]
        //public async Task<IActionResult> AddPortfolioImage([FromForm] PortfolioProjectImage image)
        //{
        //    if (!ModelState.IsValid)
        //    {
        //        return BadRequest(ModelState);
        //    }

        //    try
        //    {
        //        var imgUpload = Request.Form.Files[0];
        //        image.ImageByteArray = ByteHelper.ConvertToBytes(imgUpload);

        //        image.CreationDate = DateTime.Now;
        //        image.ModifiedDate = DateTime.Now;

        //        await _projectImageService.AddPortfolioProjectImageAsync(image);
        //    }
        //    catch (DbUpdateException)
        //    {
        //        if (_projectImageService.PortfolioImageExists(image.ID))
        //        {
        //            return new StatusCodeResult(StatusCodes.Status409Conflict);
        //        }
        //        else
        //        {
        //            throw;
        //        }
        //    }

        //    return CreatedAtAction("GetPortfolioImage", new { id = image.ID }, image);
        //}

        //[HttpDelete("{id}")]
        //public async Task<IActionResult> DeletePortfolioImage([FromRoute] int id)
        //{
        //    var image = await _projectImageService.GetPortfolioProjectImageAsync(id);
        //    if (image == null)
        //    {
        //        return NotFound();
        //    }

        //    await _projectImageService.DeletePortfolioProjectImage(id);

        //    return Ok();
        //}
    }
}