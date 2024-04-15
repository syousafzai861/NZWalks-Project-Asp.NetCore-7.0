using AutoMapper;
using NXWalks.API.Models.Domains;
using NXWalks.API.Models.DTO;

namespace NXWalks.API.Mapping
{
    public class AutoMapperProfiles : Profile
    {
        public AutoMapperProfiles()
        {
            CreateMap<Region, RegionDTO>().ReverseMap();
            CreateMap<AddRegionRequestDto, Region>().ReverseMap();
            CreateMap<UpdateRegionRequestDto,Region>().ReverseMap();
            CreateMap<AddWalkRequestDTO, Walks>().ReverseMap();
            CreateMap<Walks, WalksDTO>().ReverseMap();
            CreateMap<Difficulty,DifficultyDTO>().ReverseMap();
            CreateMap<UpdateRequestWalkDTO, Walks>().ReverseMap();
        }
    }
}
