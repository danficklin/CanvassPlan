using CanvassPlan.Server.Services.CanvasserServices;
using CanvassPlan.Shared.Models.Canvasser;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace CanvassPlan.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CanvasserController : ControllerBase
    {
        private readonly ICanvasserService _canvasserService;
        public CanvasserController(ICanvasserService canvasserService)
        {
            _canvasserService = canvasserService;
        }
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
            _canvasserService.SetUserId(userId);
            return true;
        }
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            if (!SetUserIdInService()) return Unauthorized();
            var canvassers = await _canvasserService.ListCanvassersAsync();
            return Ok(canvassers.ToList());
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> Canvasser(int id)
        {
            if (!SetUserIdInService()) return Unauthorized();
            var canvasser = await _canvasserService.GetCanvasserByIdAsync(id);
            if (canvasser == null) return NotFound();
            return Ok(canvasser);
        }
        [HttpGet("{name}")]
        public async Task<IActionResult> Canvasser(string name)
        {
            if (!SetUserIdInService()) return Unauthorized();
            var canvasser = await _canvasserService.GetCanvasserByNameAsync(name);
            if (canvasser == null) return NotFound();
            return Ok(canvasser);
        }
        [HttpPost]
        public async Task<IActionResult> Create(CanvasserCreate model)
        {
            if (!ModelState.IsValid || model is null) return BadRequest();
            if (!SetUserIdInService()) return Unauthorized();
            bool wasSuccessful = await _canvasserService.AddCanvasserAsync(model);
            if (wasSuccessful) return Ok();
            else return UnprocessableEntity();
        }
        [HttpPut("edit/{id}")]
        public async Task<IActionResult> Edit(int id, CanvasserEdit model)
        {
            if (!SetUserIdInService()) return Unauthorized();
            if (model == null || !ModelState.IsValid) return BadRequest();
            if (model.CanvasserId != id) return BadRequest();
            bool wasSuccessful = await _canvasserService.UpdateCanvasserAsync(model);
            if (wasSuccessful) return Ok();
            return BadRequest();
        }
        [HttpDelete("delete/{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            if (!SetUserIdInService()) return Unauthorized();
            var canvasser = await _canvasserService.GetCanvasserByIdAsync(id);
            if (canvasser == null) return NotFound();
            bool wasSuccessful = await _canvasserService.DeleteCanvasserAsync(id);
            if(!wasSuccessful) return BadRequest();
            return Ok();
        }
        
    }
}
