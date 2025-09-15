using ApiProject3.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebApplication1.Models;

namespace ApiProject3.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        private readonly DataContext _context;

        public CategoryController(DataContext context)
        {
            _context = context;
        }

        [HttpPost]
        public IActionResult Post(Category input)
        {
            _context.Categories.Add(input);
            _context.SaveChanges();
            return Ok(input);
        }

        [HttpGet]
        public ActionResult<List<Category>> Get()
        {
            return _context.Categories.ToList();
        }

        [HttpPut("{id}")]
        public IActionResult Put(int id, Category input)
        {
            var category = _context.Categories.Find(id);
            if (category == null) return NotFound();

            category.Name = input.Name;
            category.Description = input.Description;
            category.image = input.image;
            _context.SaveChanges();

            return Ok(category);
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var category = _context.Categories.Find(id);
            if (category == null) return NotFound();

            _context.Categories.Remove(category);
            _context.SaveChanges();

            return Ok();
        }
    }

}
