using AutoMapper;
using GeoApi.Dtos;
using GeoApi.Models;

namespace GeoApi.Profiles;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        // Country
        CreateMap<Country, CountryReadDto>();
        CreateMap<CountryCreateDto, Country>();
        CreateMap<CountryUpdateDto, Country>();

        // State
        CreateMap<State, StateReadDto>();
        CreateMap<StateCreateDto, State>();
        CreateMap<StateUpdateDto, State>();

        // City
        CreateMap<City, CityReadDto>();
        CreateMap<CityCreateDto, City>();
        CreateMap<CityUpdateDto, City>();
    }
}
