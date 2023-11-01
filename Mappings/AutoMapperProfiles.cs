using AutoMapper;
using coreAPI.Models.Domain;
using coreAPI.Models.DTO.Regions;
using coreAPI.Models.DTO.Walks;

namespace coreAPI.Mappings
{
    public class AutoMapperProfiles : Profile
    {
        public AutoMapperProfiles()
        {
            CreateMap<Region, RegionDto>().ReverseMap();
            CreateMap<AddRegionRequestDto, Region>().ReverseMap();
            CreateMap<UpdateRegionDto, Region>().ReverseMap();
            CreateMap<Walk, WalkDto>().ReverseMap();
            CreateMap<AddWalksRequestDto, Walk>().ReverseMap();
            CreateMap<UpdateWalksRequestDto, Walk>().ReverseMap();
        }
    }
}