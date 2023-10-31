using AutoMapper;
using coreAPI.Models.Domain;
using coreAPI.Models.DTO.Regions;

namespace coreAPI.Mappings
{
    public class AutoMapperProfiles : Profile
    {
        public AutoMapperProfiles()
        {
            CreateMap<Region, RegionDto>().ReverseMap();
            CreateMap<AddRegionRequestDto, Region>().ReverseMap();
            CreateMap<UpdateRegionDto, Region>().ReverseMap();

        }
    }
}