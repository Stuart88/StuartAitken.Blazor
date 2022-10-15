using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using StuartAitken.Blazor.Server.DataAccess.Entities;
using StuartAitken.Blazor.Server.DataService;
using StuartAitken.Blazor.Server.Helpers;
using StuartAitken.Blazor.Shared.Models;

namespace StuartAitken.Blazor.Server.Controllers
{
    [Route("api/projects/")]
    [ApiController]
    public class ProjectsController : ControllerBase
    {
        private ProjectsService _projectsService;
        private ProjectImageService _projectImageService;

        public ProjectsController(
            ProjectsService projectsService,
            ProjectImageService projectImageService
        )
        {
            this._projectsService = projectsService;
            this._projectImageService = projectImageService;
        }

        #region Portfolio Projects

        [HttpGet("all")]
        public IEnumerable<Project> GetAllPortfolioProjects()
        {
            var projects = _projectsService.GetAllPortfolioProjects();

            return projects;
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

        [HttpGet("types")]
        public async Task<List<ProjectType>> GetProjectTypes()
        {
            var types = await _projectsService.GetProjectTypes();

            return types;
        }

        [HttpGet("techs")]
        public async Task<List<ProjectTech>> GetProjectTechs()
        {
            var techs = await _projectsService.GetProjectTechs();

            return techs;
        }

        //[HttpPost("viewedProject")]
        //public async Task<IActionResult> ViewedProject([FromBody] int id)
        //{
        //    var portfolioProject = await _projectsService.GetPortfolioProject(id);

        //    if (portfolioProject == null)
        //    {
        //        return NotFound();
        //    }

        //    portfolioProject.Views = portfolioProject.Views ?? 0;
        //    portfolioProject.Views++;

        //    await _projectsService.UpdatePortfolioProject(portfolioProject);

        //    return Ok();
        //}

        //// PUT
        //[HttpPut("{id}")]
        //public async Task<IActionResult> EditPortfolioProject(
        //    [FromRoute] int ID,
        //    [FromForm] PortfolioProject portfolioProject
        //)
        //{
        //    if (!ModelState.IsValid)
        //    {
        //        return BadRequest(ModelState);
        //    }

        //    if (ID != portfolioProject.ID)
        //    {
        //        return BadRequest();
        //    }
        //    try
        //    {
        //        if (await _projectsService.UpdatePortfolioProject(portfolioProject) != 0)
        //        {
        //            //check for image in Form (will always be second file in form)
        //            if (Request.Form.Files.Count > 0)
        //            {
        //                foreach (IFormFile file in Request.Form.Files)
        //                {
        //                    await _projectImageService.AddNewProjectImageAsync(
        //                        ByteHelper.ConvertToBytes(file),
        //                        ID
        //                    );
        //                }
        //            }

        //            return CreatedAtAction(
        //                "EditPortfolioProject",
        //                new { ID = portfolioProject.ID },
        //                portfolioProject
        //            );
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

        //// POST
        //[HttpPost]
        //public async Task<IActionResult> AddPortfolioProject(
        //    [FromForm] PortfolioProject portfolioProject
        //)
        //{
        //    portfolioProject.CreationDate = DateTime.Now;
        //    portfolioProject.ModifiedDate = DateTime.Now;

        //    if (!ModelState.IsValid)
        //    {
        //        return BadRequest(ModelState);
        //    }

        //    try
        //    {
        //        await _projectsService.AddPortfolioProject(portfolioProject);

        //        if (Request.Form.Files.Count > 0)
        //        {
        //            //if form data has anything else, it's an image! So add it.
        //            PortfolioProject justAdded = await _projectsService.GetPortfolioProject(
        //                portfolioProject
        //            );
        //            foreach (IFormFile file in Request.Form.Files)
        //            {
        //                await _projectImageService.AddNewProjectImageAsync(
        //                    ByteHelper.ConvertToBytes(file),
        //                    justAdded.ID
        //                );
        //            }
        //        }
        //    }
        //    catch (DbUpdateException)
        //    {
        //        if (_projectsService.PortfolioProjectExists(portfolioProject.ID))
        //        {
        //            return new StatusCodeResult(StatusCodes.Status409Conflict);
        //        }
        //        else
        //        {
        //            throw;
        //        }
        //    }

        //    return CreatedAtAction(
        //        "GetPortfolioProject",
        //        new { ID = portfolioProject.ID },
        //        portfolioProject
        //    );
        //}

        //[HttpDelete("{id}")]
        //public async Task<IActionResult> DeletePortfolioProject([FromRoute] int id)
        //{
        //    var portfolioProject = await _projectsService.GetPortfolioProject(id);
        //    if (portfolioProject == null)
        //    {
        //        return NotFound();
        //    }
        //    //List<PortfolioProjectImage> projectImages = _projectsService.GetAllPortfolioProjectImages().Where(x => x.ProjectID == ID).ToList();

        //    //foreach(PortfolioProjectImage img in projectImages)
        //    //{
        //    //    await _projectsService.DeletePortfolioProjectImage(img.ID);
        //    //}

        //    await _projectsService.DeletePortfolioProjectAsync(id);

        //    return Ok(portfolioProject);
        //}

        //// POST
        //[HttpPost]
        //[AcceptVerbs("POST", "post")]
        //[Route("api/StuartAitkenWebsite/masterlist")]
        //public async Task<IActionResult> UploadMasterList()
        //{
        //    if (Request.Form.Files.Count > 0)
        //    {
        //        try
        //        {
        //            string currentDir = Environment.CurrentDirectory;
        //            string appPath = Path.Combine(
        //                currentDir,
        //                "wwwroot",
        //                "dist",
        //                "assets",
        //                "downloads"
        //            );

        //            string filePath = "";

        //            var file = Request.Form.Files[0];

        //            //add new files
        //            if (file.Length > 0)
        //            {
        //                if (file.FileName != "masterList.json")
        //                    throw new Exception(
        //                        $"Filename incorrect! ({file.FileName}). Should be 'masterList.json'"
        //                    );

        //                string fileName = file.FileName;

        //                filePath = Path.Combine(appPath, "masterList.json");

        //                using (FileStream fileStream = new FileStream(filePath, FileMode.Create))
        //                {
        //                    await file.CopyToAsync(fileStream);
        //                }
        //            }

        //            return new JsonResult(new { ok = true, message = "" });
        //        }
        //        catch (Exception e)
        //        {
        //            return new JsonResult(new { ok = false, message = e.ToString() });
        //        }
        //    }
        //    else
        //    {
        //        return NoContent();
        //    }
        //}

        #endregion
    }
}
