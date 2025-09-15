using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using secondproject.modals;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace secondproject.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CarController : ControllerBase
    {
        static List<Car> cars = new List<Car>();
        [HttpPost]
        public string karanveer(Car car)
        {
            cars.Add(car);
            return "Car added successfuly";
        }
        [HttpGet]
        public List<Car> none()
        {
            return cars;
        }
        [HttpPut]
        public string Put(int index, string Model, string brand,string color,string engine)
        {
            if (index >= 0 && index < cars.Count)
            {
                cars[index].modal = Model;
                cars[index].brand = brand;
                cars[index].color = color;
                cars[index].engine = engine;
                return "Car updated successfully";
            }
            else
            {
                return "Invalid index";
            }
        }
        [HttpDelete]
        public string Delete(int index)
        {
            cars.RemoveAt(index);
            return "Deleted successfuly";
        }
    }
}

