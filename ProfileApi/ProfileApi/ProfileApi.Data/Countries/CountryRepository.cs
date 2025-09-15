
using ProfileApi.Data;

namespace ProfileApi.Countries
{
    public class CountryRepository : ICountryRepository
    {
        private readonly DataContext _context;
        public CountryRepository(DataContext context)
        {
            _context = context;
        }
        public async Task<Country>  CreateCountry(Country country)
        {

            _context.Countries.Add(country);
           await  _context.SaveChangesAsync();
           return country;
        }
    }
}
