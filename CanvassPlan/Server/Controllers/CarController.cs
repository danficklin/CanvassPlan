using CanvassPlan.Server.Services.CarServices;
using CanvassPlan.Shared.Models.Car;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace CanvassPlan.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CarController : ControllerBase
    {
        private readonly ICarService _carService;
        public CarController(ICarService carService) { _carService = carService; }
        private string GetUserId()
        {
            string userIdClaim = User.Claims.First(i => i.Type == ClaimTypes.NameIdentifier).Value;
            if (userIdClaim == null) return null;
            return userIdClaim;
        }
        private bool SetUserIdInService()
        {
            var userId = GetUserId();
            if (userId == null) return false;
            _carService.SetUserId(userId);
            return true;
        }
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            if (!SetUserIdInService()) return Unauthorized();
            var cars = await _carService.ListCarsAsync();
            return Ok(cars.ToList());
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> Car(int id)
        {
            if (!SetUserIdInService()) return Unauthorized();
            var car = await _carService.GetCarByIdAsync(id);
            if (car == null) return NotFound();
            return Ok(car);
        }
        [HttpGet("/name/{name}")]
        public async Task<IActionResult> Car(string name)
        {
            if (!SetUserIdInService()) return Unauthorized();
            var car = await _carService.GetCarByNameAsync(name);
            if (car == null) return NotFound();
            return Ok(car);
        }
        [HttpPost]
        public async Task<IActionResult> Create(CarCreate model)
        {
            if (!ModelState.IsValid || model is null) return BadRequest();
            if (!SetUserIdInService()) return Unauthorized();
            bool wasSuccessful = await _carService.AddCarAsync(model);
            if (wasSuccessful) return Ok();
            else return UnprocessableEntity();
        }
        [HttpPut("edit/{id}")]
        public async Task<IActionResult> Edit(int id, CarEdit model)
        {
            if (!SetUserIdInService()) return Unauthorized();
            if (model == null || !ModelState.IsValid) return BadRequest();
            if (model.CarId != id) return BadRequest();
            bool wasSuccessful = await _carService.UpdateCarAsync(model);
            if (wasSuccessful) return Ok();
            return BadRequest();
        }

        [HttpPut("active/{id}")]
        public async Task<IActionResult> ToggleActive(int id)
        {
            if (!SetUserIdInService()) return Unauthorized();
            if (id == default || !ModelState.IsValid) return BadRequest();
            bool wasSuccessful = await _carService.ToggleCarActiveAsync(id);
            if (wasSuccessful) return Ok();
            return BadRequest();
        }

        [HttpDelete("delete/{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            if (!SetUserIdInService()) return Unauthorized();
            var canvasser = await _carService.GetCarByIdAsync(id);
            if (canvasser == null) return NotFound();
            bool wasSuccessful = await _carService.DeleteCarAsync(id);
            if (!wasSuccessful) return BadRequest();
            return Ok();
        }

        [HttpPut("team/{id}")]
        public async Task<IActionResult> AddCarToTeam(int id, CarAddToTeam model)
        {
            if (!SetUserIdInService()) return Unauthorized();
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            return await _carService.AddCarToTeamAsync(id, model)
                ? Ok("Car was added to the team successfully!")
                : BadRequest("Car could not be added to the team. Please try again.");
        }
    }
}
