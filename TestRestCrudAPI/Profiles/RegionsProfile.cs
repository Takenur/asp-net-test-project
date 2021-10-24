using AutoMapper;
using TestRestCrudAPI.Dtos;
using TestRestCrudAPI.Models;

namespace TestRestCrudAPI.Profiles
{
    public class RegionsProfile :Profile
    {
        public RegionsProfile()
        {
            // Sourse->Target
            CreateMap<Regions, RegionsReadDto>();
            CreateMap<RegionsCreateDto, Regions>();
            CreateMap<RegionsUpdateDto, Regions>();
            CreateMap<Regions, RegionsUpdateDto>();
        }
    }
}
