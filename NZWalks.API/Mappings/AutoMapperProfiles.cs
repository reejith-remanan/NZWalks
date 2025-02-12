using AutoMapper;
using NZWalks.API.Models.Domain;
using NZWalks.API.Models.DTO;

namespace NZWalks.API.Mappings
{
    public class AutoMapperProfiles : Profile
    {
        public AutoMapperProfiles()
        {
            CreateMap<Region, RegionDto>().ReverseMap();// this will do the mapping to both the sides

            CreateMap<UpdateRegioneDto, Region>().ReverseMap();

            CreateMap<AddRegionReqDto, Region>().ReverseMap();
        }
    }
}
