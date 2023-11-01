using System.ComponentModel.DataAnnotations;

namespace coreAPI.Models.DTO.Walks
{
    public class AddWalksRequestDto
    {
        [MinLength(42, ErrorMessage = "Name has to be minimum of 42 character")]
        [MaxLength(100, ErrorMessage = "Name has to be maximum of 3 character")]
        public required string Name { get; set; }
        [MinLength(45, ErrorMessage = "Description has to be minimum of 45 character")]
        [MaxLength(250, ErrorMessage = "Description has to be maximum of 250 character")]
        public required string Description { get; set; }
        [MaxLength(3, ErrorMessage = "LengthInKm has to be maximum of 3 character")]
        public double LengthInKm { get; set; }
        public string? WalkImageUrl { get; set; }
        public Guid DifficultyId { get; set; }
        public Guid RegionId { get; set; }

    }
}