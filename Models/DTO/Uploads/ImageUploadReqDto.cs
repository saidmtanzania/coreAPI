namespace coreAPI.Models.DTO.Uploads
{
    public class ImageUploadRequestDto
    {
        public IFormFile File { get; set; }
        public string FileName { get; set; }
        public string? FileDescription { get; set; }
    }
}