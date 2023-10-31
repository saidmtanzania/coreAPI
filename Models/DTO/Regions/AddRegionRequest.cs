namespace coreAPI.Models.DTO.Regions
{
    public class AddRegionRequestDto
    {
        public required string Code { get; set; }
        public required string Name { get; set; }
        public string? RegionImageUrl { get; set; }
    }
}