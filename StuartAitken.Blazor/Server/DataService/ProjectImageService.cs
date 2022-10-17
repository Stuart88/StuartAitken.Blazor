using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using StuartAitken.Blazor.Client.Pages;
using StuartAitken.Blazor.Server.DataAccess.Entities;
using StuartAitken.Blazor.Server.Mapper;
using StuartAitken.Blazor.Shared.Models;

namespace StuartAitken.Blazor.Server.DataService
{
    public class ProjectImageService : ServiceBase
    {
        #region Public Constructors

        public ProjectImageService()
        { }

        #endregion Public Constructors

        #region Public Methods

        public async Task<ProjectImage> AddPortfolioProjectImageAsync(
            string imageRootPath,
            IFormFile image,
            int projectID
        )
        {
            if (image.Length == 0)
                throw new Exception("No image data!");

            if (image.Length > Shared.Constants.Constants.MaxFileSizeBytes)
                throw new Exception("Image too large! (> 3)");

            try
            {
                var portfolioProjectImage = new PortfolioProjectImage
                {
                    CreationDate = DateTime.Now,
                    ProjectId = projectID,
                    Status = 1,
                    ModifiedDate = DateTime.Now,
                };

                _db.Add(portfolioProjectImage);
                await _db.SaveChangesAsync();


                string imagePath = Path.Combine(imageRootPath, $"{portfolioProjectImage.ID}.png");

                await using FileStream fs = new(imagePath, FileMode.Create);
                await image.CopyToAsync(fs);

                return Mapper.Mapper.Map<PortfolioProjectImage, ProjectImage>(portfolioProjectImage);
            }
            catch
            {
                throw;
            }
        }

        public async Task DeleteAllImagesForProjectId(string imageFolder, int id)
        {
            try
            {
                var images = _db.PortfolioProjectImages.Where(i => i.ProjectId == id);
                foreach(var img in images)
                {
                    File.Delete(Path.Combine(imageFolder, $"{img.ID}.png"));
                }
                _db.PortfolioProjectImages.RemoveRange(images);
                await _db.SaveChangesAsync();
            }
            catch
            {
                throw;
            }
        }

        public async Task<int> DeletePortfolioProjectImage(string imageFolder, int id)
        {
            try
            {
                PortfolioProjectImage? portfolioProjectImage = _db.PortfolioProjectImages.Find(id);

                if (portfolioProjectImage != null)
                {
                    File.Delete(Path.Combine(imageFolder, $"{id}.png"));

                    _db.PortfolioProjectImages.Remove(portfolioProjectImage);
                    _db.Entry(portfolioProjectImage).State = EntityState.Deleted;
                    return await _db.SaveChangesAsync();
                }

                return 0;
            }
            catch
            {
                throw;
            }
        }

        public IEnumerable<ProjectImage> GetAllPortfolioProjectImages()
        {
            try
            {
                IEnumerable<ProjectImage> allProjects = _db.PortfolioProjectImages
                    .Map<PortfolioProjectImage, ProjectImage>()
                    .ToList();

                return allProjects;
            }
            catch
            {
                throw;
            }
        }

        public async Task<ProjectImage?> GetMainPortfolioProjectImageAsync(int projID)
        {
            try
            {
                return await _db.PortfolioProjectImages
                    .Where(img => img.PrimaryImage == 1 && img.ProjectId == projID)
                    .Map<PortfolioProjectImage, ProjectImage>()
                    .FirstOrDefaultAsync();
            }
            catch
            {
                throw;
            }
        }

        public async Task<ProjectImage?> GetPortfolioProjectImageAsync(int id)
        {
            try
            {
                var img = await _db.PortfolioProjectImages
                    .Where(i => i.ID == id)
                    .Map<PortfolioProjectImage, ProjectImage>()
                    .FirstOrDefaultAsync();

                return img;
            }
            catch
            {
                throw;
            }
        }

        public async Task<ProjectImage?> GetPortfolioProjectImageFromProjectIDAsync(int projectID)
        {
            try
            {
                return await _db.PortfolioProjectImages
                    .Where(x => x.ProjectId == projectID)
                    .Map<PortfolioProjectImage, ProjectImage>()
                    .FirstOrDefaultAsync();
            }
            catch
            {
                throw;
            }
        }

        public IEnumerable<int> GetProjectImageIDs(int projID)
        {
            try
            {
                return _db.PortfolioProjectImages
                    .Where(x => x.ProjectId == projID && x.PrimaryImage != 1)
                    .Select(s => s.ID);
            }
            catch
            {
                throw;
            }
        }

        public List<ProjectImage> GetProjectImages(int projID)
        {
            try
            {
                var images = _db.PortfolioProjectImages.Where(x => x.ProjectId == projID);

                return images.Map<PortfolioProjectImage, ProjectImage>().ToList();
            }
            catch
            {
                throw;
            }
        }

        public bool PortfolioImageExists(int id)
        {
            return _db.PortfolioProjectImages.Any(e => e.ID == id);
        }

        public async Task<int> SetMainPortfolioProjectImageAsync(int id)
        {
            try
            {
                PortfolioProjectImage? newMainImage = _db.PortfolioProjectImages.Find(id);

                if (newMainImage == null)
                {
                    throw new Exception("Image not found");
                }

                int projID = newMainImage.ProjectId;

                var oldMainImages = _db.PortfolioProjectImages
                    .Where(x => x.ProjectId == projID && x.PrimaryImage == 1)
                    .ToList(); // Should only be one but do to list just to be sure

                if (oldMainImages.Count > 0)
                {
                    oldMainImages.ForEach(img =>
                    {
                        img.PrimaryImage = 0;
                        _db.Entry(img).State = EntityState.Modified;
                    });
                }

                newMainImage.PrimaryImage = 1;

                _db.Entry(newMainImage).State = EntityState.Modified;

                return await _db.SaveChangesAsync();
            }
            catch
            {
                throw;
            }
        }

        public async Task<int> UpdatePortfolioProjectImageAsync(
                            PortfolioProjectImage portfolioProjectImage
        )
        {
            try
            {
                _db.Entry(portfolioProjectImage).State = EntityState.Modified;
                return await _db.SaveChangesAsync();
            }
            catch
            {
                throw;
            }
        }

        public async Task UpdateProjectImageAsync(byte[] image, PortfolioProjectImage updating)
        {
            // TO DO
            // Save update image in file system with name {imageId}.png

            updating.ModifiedDate = DateTime.Now;

            await UpdatePortfolioProjectImageAsync(updating);
        }

        #endregion Public Methods
    }
}