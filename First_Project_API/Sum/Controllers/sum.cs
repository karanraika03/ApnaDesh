using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Sum.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class sum : ControllerBase
    {
        [HttpGet]
        public int sumof(int a, int b)
        {
            return a + b;
        }
    }
    [Route("api/[controller]")]
    [ApiController]
    public class sub : ControllerBase
    {
        [HttpGet]
        public int subof(int a, int b)
        {
            return a - b;
        }
    }
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class Multi : ControllerBase
    {
        [HttpGet]
        public int multiof(int a, int b)
        {
            return a * b;
        }
    }
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class Div : ControllerBase
    {
        [HttpGet]
        public int divof(int a, int b)
        {
            return a / b;
        }
    }
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class Remainder : ControllerBase
    {
        [HttpGet]
        public int remainderof(int a, int b)
        {
            return a % b;
        }
    }
}
