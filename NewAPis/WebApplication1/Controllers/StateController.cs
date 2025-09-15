using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebApplication1.Models;

namespace WebApplication1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StateController : ControllerBase
    {
        private readonly DataContext _context;

        public StateController(DataContext context)
        {
            _context = context;
        }
        [HttpPost]
        public void Post(State input)
        {
            _context.States.Add(input);
            _context.SaveChanges();
        }
        [HttpGet]
        public List<State> Get()
        {
            return _context.States.ToList();
        }
        [HttpGet("{id}")]
        public State GetState(int id)
        {
            return _context.States.Find(id);
        }
        [HttpPut("{id}")]
        public void Put(int id, State input)
        {
            State state = _context.States.Find(id);

            if (state != null)
            {
                state.Name = input.Name;
                _context.SaveChanges();
            }
        }
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            State state = _context.States.Find(id);

            if (state != null)
            {
                _context.States.Remove(state);
                _context.SaveChanges();
            }

        }
    }
}
