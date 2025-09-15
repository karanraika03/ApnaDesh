namespace ProfileApi.Countries;

public interface ICountryRepository
{
    Task<Country> CreateCountry(Country country);
}
