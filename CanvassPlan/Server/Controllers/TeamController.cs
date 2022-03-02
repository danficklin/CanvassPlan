using CanvassPlan.Server.Services.TeamServices;
using CanvassPlan.Shared.Models.Team;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace CanvassPlan.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TeamController : ControllerBase
    {
        private readonly ITeamService _teamService;
        public TeamController(ITeamService teamService)
        {
            _teamService = teamService;
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
            _teamService.SetUserId(userId);
            return true;
        }
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            if (!SetUserIdInService()) return Unauthorized();
            var teams = await _teamService.ListTeamAsync();
            return Ok(teams.ToList());
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> Team(int id)
        {
            if (!SetUserIdInService()) return Unauthorized();
            var team = await _teamService.GetTeamByIdAsync(id);
            if (team == null) return NotFound();
            return Ok(team);
        }
        [HttpGet("/name/{name}")]
        public async Task<IActionResult> Team(string name)
        {
            if (!SetUserIdInService()) return Unauthorized();
            var team = await _teamService.GetTeamByNameAsync(name);
            if (team == null) return NotFound();
            return Ok(team);
        }
        [HttpPost]
        public async Task<IActionResult> Create(TeamCreate model)
        {
            if (!ModelState.IsValid || model is null) return BadRequest();
            if (!SetUserIdInService()) return Unauthorized();
            bool wasSuccessful = await _teamService.AddTeamAsync(model);
            if (wasSuccessful) return Ok();
            else return UnprocessableEntity();
        }
        [HttpPut("edit/{id}")]
        public async Task<IActionResult> Edit(int id, TeamEdit model)
        {
            if (!SetUserIdInService()) return Unauthorized();
            if (model == null || !ModelState.IsValid) return BadRequest();
            if (model.TeamId != id) return BadRequest();
            bool wasSuccessful = await _teamService.UpdateTeamAsync(model);
            if (wasSuccessful) return Ok();
            return BadRequest();
        }
        [HttpDelete("delete/{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            if (!SetUserIdInService()) return Unauthorized();
            var team = await _teamService.GetTeamByIdAsync(id);
            if (team == null) return NotFound();
            bool wasSuccessful = await _teamService.DeleteTeamAsync(id);
            if (!wasSuccessful) return BadRequest();
            return Ok();
        }
    }
}
