
using Microsoft.AspNetCore.Mvc;
using WebApplication1.Models;

namespace WebApplication1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CountryController : ControllerBase
    {
        private readonly DataContext _context;

        public CountryController(DataContext context)
        {
            _context = context;
        }
        [HttpPost]
        public void Post(Country input)
        {
            _context.Countries.Add(input);
            _context.SaveChanges();
        }
        [HttpGet]
        public List<Country> Get()
        {
            return _context.Countries.ToList();
        }
        [HttpGet("{id}")]
        public Country GetCountry(int id)
        {
            return _context.Countries.Find(id);
        }
        [HttpPut("{id}")]
        public void Put(int id, Country input)
        {
            Country country = _context.Countries.Find(id);

            if (country != null)
            {
                country.Name = input.Name;
                _context.SaveChanges();
            }
        }
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            Country country = _context.Countries.Find(id);

            if (country != null)
            {
                _context.Countries.Remove(country);
                _context.SaveChanges();
            }

        }
    }
}
