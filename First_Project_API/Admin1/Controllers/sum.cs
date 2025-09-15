using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Admin1.Controllers
{
    [Route("/api-my/[controller]/[action]")]
    [ApiController]
    public class sum : ControllerBase
    {
        [HttpGet]
        public int add(int a,int b)
        {
            return a + b;
        }
    }
}
