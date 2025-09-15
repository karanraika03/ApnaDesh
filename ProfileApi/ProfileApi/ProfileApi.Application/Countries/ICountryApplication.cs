namespace ProfileApi.Countries;

public interface ICountryApplication
{
    Task<CountryDto> CreateCountry(CreateUpdateCountryDto country);
}
