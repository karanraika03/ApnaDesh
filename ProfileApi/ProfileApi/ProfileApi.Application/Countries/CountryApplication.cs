
namespace ProfileApi.Countries;

public class CountryApplication : ICountryApplication
{
    private readonly ICountryRepository _countryRepository;
    public CountryApplication(ICountryRepository countryRepository)
    {
        _countryRepository = countryRepository;
    }

    public async Task<CountryDto>  CreateCountry(CreateUpdateCountryDto input)
    {
        var country = new Country();
        country.Name = input.Name;
        country.Createddate =DateTime.Now;
       var result=  await _countryRepository.CreateCountry(country);

        var countryDto = new CountryDto();

        countryDto.Id = result.Id;
        countryDto.Name = result.Name;
        countryDto.Createddate = result.Createddate;
        countryDto.Updateddate = result.Updateddate;


        return countryDto;

    }
}
 


