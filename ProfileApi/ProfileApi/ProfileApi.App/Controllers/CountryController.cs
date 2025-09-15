using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ProfileApi.Countries;

namespace ProfileApi.App.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CountryController : ControllerBase
    {
        private readonly ICountryApplication _countryApplication;

        public CountryController(ICountryApplication countryApplication)
        {
            _countryApplication = countryApplication;
        }
        [HttpPost]
        public async Task<ActionResult<CountryDto>> Post(CreateUpdateCountryDto input)
        {
           
          return  await _countryApplication.CreateCountry(input);   

        }
    }
}
