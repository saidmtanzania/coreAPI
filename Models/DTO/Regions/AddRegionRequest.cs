using System.ComponentModel.DataAnnotations;

namespace coreAPI.Models.DTO.Regions
{
    public class AddRegionRequestDto
    {
        [MinLength(3, ErrorMessage = "Code has to be minimum of 3 character")]
        [MaxLength(3, ErrorMessage = "Code has to be maximum of 3 character")]
        public required string Code { get; set; }

        [MaxLength(100, ErrorMessage = "Name has to be maximum of 100 character")]
        public required string Name { get; set; }
        public string? RegionImageUrl { get; set; }
    }
}