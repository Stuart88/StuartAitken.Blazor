using Microsoft.EntityFrameworkCore;
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

        public async Task AddNewProjectImageAsync(byte[] image, int projectID)
        {
            // TO DO
            // Save image to file system with name {imageId}.png

            PortfolioProjectImage newImage = new PortfolioProjectImage
            {
                ProjectId = projectID,
                CreationDate = DateTime.Now,
                ModifiedDate = DateTime.Now,
                Status = 1,
            };
            await AddPortfolioProjectImageAsync(newImage);
        }

        public async Task<int> AddPortfolioProjectImageAsync(
            PortfolioProjectImage portfolioProjectImage
        )
        {
            try
            {
                _db.Add(portfolioProjectImage);
                return await _db.SaveChangesAsync();
            }
            catch
            {
                throw;
            }
        }

        public async Task DeleteAllImagesForProjectId(int id)
        {
            try
            {
                var images = _db.PortfolioProjectImages.Where(i => i.ProjectId == id);
                _db.PortfolioProjectImages.RemoveRange(images);
                await _db.SaveChangesAsync();
            }
            catch
            {
                throw;
            }
        }

        public async Task<int> DeletePortfolioProjectImage(int id)
        {
            try
            {
                PortfolioProjectImage? portfolioProjectImage = _db.PortfolioProjectImages.Find(id);

                if (portfolioProjectImage != null)
                {
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

        public IEnumerable<ProjectImage> GetProjectImages(int projID)
        {
            try
            {
                var images = _db.PortfolioProjectImages.Where(x => x.ProjectId == projID);

                return images.Map<PortfolioProjectImage, ProjectImage>();
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

                PortfolioProjectImage? oldMainImage = _db.PortfolioProjectImages
                    .Where(x => x.ProjectId == projID && x.PrimaryImage == 1)
                    .FirstOrDefault();

                if (oldMainImage != null)
                {
                    oldMainImage.PrimaryImage = 0;
                    _db.Entry(oldMainImage).State = EntityState.Modified;
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