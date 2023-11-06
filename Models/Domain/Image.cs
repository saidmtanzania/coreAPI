using System.ComponentModel.DataAnnotations.Schema;

namespace coreAPI.Models.Domain
{
    public class Image
    {
        public Guid Id { get; set; }
        [NotMapped]
        public required IFormFile File { get; set; }
        public required string FileName { get; set; }
        public required string FileDescription { get; set; }
        public required string FileExtension { get; set; }
        public required string FileSizeInBytes { get; set; }
        public required string FilePath { get; set; }
    }
}