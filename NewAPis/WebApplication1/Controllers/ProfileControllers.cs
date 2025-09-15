using Microsoft.AspNetCore.Mvc;
using WebApplication1.Models;

namespace WebApplication1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProfileControllers : ControllerBase
    {
        private readonly DataContext _context;

        public ProfileControllers(DataContext context)
        {
            _context = context;
        }
        [HttpPost]
        public void Post(Profile input)
        {
            _context.Profiles.Add(input);

            _context.SaveChanges();
        }
        [HttpGet]
        public List<Profile> Get()
        {
            return _context.Profiles.ToList();
        }
        [HttpGet("{id}")]
        public Profile GetProfile(int id)
        {
            return _context.Profiles.Find(id);
        }
        [HttpPut("{id}")]
        public void Put(int id, Profile input)
        {
            Profile profile = _context.Profiles.Find(id);

            if (profile != null)
            {
                profile.Name = input.Name;
                profile.DateOfBirth = input.DateOfBirth;
                profile.Email = input.Email;
                profile.Gender = input.Gender;
                profile.Phone = input.Phone;
                _context.Profiles.Update(profile);

                _context.SaveChanges();
            }
        }
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            Profile profile = _context.Profiles.Find(id);

            if (profile != null)
            {
                _context.Profiles.Remove(profile);
                _context.SaveChanges();
            }

        }
    }
}