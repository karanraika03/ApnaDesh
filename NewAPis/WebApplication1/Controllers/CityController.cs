using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebApplication1.Models;

namespace WebApplication1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CityController : ControllerBase
    {
        private readonly DataContext _context;

        public CityController(DataContext context)
        {
            _context = context;
        }
        [HttpPost]
        public void Post(City input)
        {
            _context.Cities.Add(input);
            _context.SaveChanges();
        }
        [HttpGet]
        public List<City> GetData()
        {
            return _context.Cities.ToList();
        }
        [HttpGet("{id}")]
        public City GetCity(int id)
        {
            return _context.Cities.Find(id);
        }
        [HttpPut("{id}")]
        public void PutData(int id, City input)
        {
            City city = _context.Cities.Find(id);

            if (city != null)
            {
                city.Name = input.Name;
                _context.SaveChanges();
            }
        }
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            City city = _context.Cities.Find(id);

            if (city != null)
            {
                _context.Cities.Remove(city);
                _context.SaveChanges();
            }

        }
    }
}
