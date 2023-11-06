using coreAPI.Data;
using coreAPI.Models.Domain;

namespace coreAPI.Repositories.Uploads
{
    public class LocalImageRepository : IImageRepository
    {
        private readonly IWebHostEnvironment _hostEnvironment;
        private readonly IHttpContextAccessor _contextAccessor;
        private readonly CoreDbContext _dbContext;

        public LocalImageRepository(IWebHostEnvironment hostEnvironment, IHttpContextAccessor contextAccessor, CoreDbContext dbContext)
        {
            _hostEnvironment = hostEnvironment;
            _contextAccessor = contextAccessor;
            _dbContext = dbContext;
        }
        public async Task<Image> Upload(Image image)
        {
            try
            {
                var uploadDirectory = Path.Combine(_hostEnvironment.ContentRootPath, "UploadFolder", "Images");
                var localPath = Path.Combine(uploadDirectory, $"{image.FileName}{image.FileExtension}");

                // Upload image to Local Path
                using var stream = new FileStream(localPath, FileMode.Create);
                await image.File.CopyToAsync(stream);

                var urlFilePath = $"{_contextAccessor.HttpContext.Request.Scheme}://{_contextAccessor.HttpContext.Request.Host}{_contextAccessor.HttpContext.Request.PathBase}/UploadFolder/Images/{image.FileName}{image.FileExtension}";
                image.FilePath = urlFilePath;

                // Add Image to Images Table
                await _dbContext.Images.AddAsync(image);
                await _dbContext.SaveChangesAsync();

                return image; // Return the image on success
            }
            catch (Exception ex)
            {
                // Handle exceptions here, e.g., log the error
                throw ex; // Optionally, rethrow the exception
            }
        }

        // public async Task<Image> Upload(Image image)
        // {
        //     var uploadDirectory = Path.Combine(_hostEnvironment.ContentRootPath, "UploadFolder", "Images");
        //     var localPath = Path.Combine(uploadDirectory, image.FileName, image.FileExtension);

        //     //Upload image to Local Path
        //     using var stream = new FileStream(localPath, FileMode.Create);
        //     await image.File.CopyToAsync(stream);

        //     //
        //     var urlFilePath = $"{_contextAccessor.HttpContext.Request.Scheme}://{_contextAccessor.HttpContext.Request.Host}{_contextAccessor.HttpContext.Request.PathBase}/UploadFolder/Images/{image.FileName}{image.FileExtension}";
        //     image.FilePath = urlFilePath;
        //     //Add Images to IMmages Table
        //     await _dbContext.Images.AddAsync(image);
        //     await _dbContext.SaveChangesAsync();

        // }
    }
}