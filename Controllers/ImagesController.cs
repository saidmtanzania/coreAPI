using AutoMapper;
using coreAPI.Models.Domain;
using coreAPI.Models.DTO.Uploads;
using coreAPI.Repositories.Uploads;
using Microsoft.AspNetCore.Mvc;

namespace coreAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ImagesController : ControllerBase
    {
        private readonly IImageRepository _imageRepository;

        public ImagesController(IImageRepository imageRepository)
        {
            _imageRepository = imageRepository;
        }
        [HttpPost]
        [Route("upload")]
        public async Task<IActionResult> Upload([FromForm] ImageUploadRequestDto imageUpload)
        {
            ValidateImageUpload(imageUpload);
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            //Convert DTO to Domain
            // var imageDomain = _mapper.Map<Image>(imageUpload);
            Image imageDomain = new Image
            {
                File = imageUpload.File,
                FileExtension = Path.GetExtension(imageUpload.File.FileName),
                FileSizeInBytes = imageUpload.File.Length.ToString(),
                FileName = imageUpload.FileName,
                FileDescription = imageUpload.FileDescription,
            };

            //User repository to upload Image
            await _imageRepository.Upload(imageDomain);

            //return response
            return Ok(imageDomain);
        }

        private void ValidateImageUpload(ImageUploadRequestDto imageUpload)
        {
            string[] allowedExtension = new string[] { ".jpg", ".jpeg", ".png" };
            if (!allowedExtension.Contains(Path.GetExtension(imageUpload.File.FileName)))
            {
                ModelState.AddModelError("File", "Invalid file type");
            }

            if (imageUpload.File.Length > 10485760)
            {
                ModelState.AddModelError("File", "File size cannot exceed 10MB");
            }
        }
    }
}