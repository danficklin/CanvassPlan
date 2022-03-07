using CanvassPlan.Server.Services.SiteServices;
using CanvassPlan.Shared.Models.Site;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace CanvassPlan.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SiteController : ControllerBase
    {
        private readonly ISiteService _siteService;
        public SiteController(ISiteService siteService)
        {
            _siteService = siteService;
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
            _siteService.SetUserId(userId);
            return true;
        }
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            if (!SetUserIdInService()) return Unauthorized();
            var sites = await _siteService.ListSitesAsync();
            return Ok(sites.ToList());
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> Site(int id)
        {
            if (!SetUserIdInService()) return Unauthorized();
            var site = await _siteService.GetSiteByIdAsync(id);
            if (site == null) return NotFound();
            return Ok(site);
        }

        [HttpGet("/name/{name}")]
        public async Task<IActionResult> Site(string name)
        {
            if (!SetUserIdInService()) return Unauthorized();
            var site = await _siteService.GetSiteByNameAsync(name);
            if (site == null) return NotFound();
            return Ok(site);
        }
        [HttpPost]
        public async Task<IActionResult> Create(SiteCreate model)
        {
            if (!ModelState.IsValid || model is null) return BadRequest();
            if (!SetUserIdInService()) return Unauthorized();
            bool wasSuccessful = await _siteService.AddSiteAsync(model);
            if (wasSuccessful) return Ok();
            else return UnprocessableEntity();
        }
        [HttpPut("edit/{id}")]
        public async Task<IActionResult> Edit(int id, SiteEdit model)
        {
            if (!SetUserIdInService()) return Unauthorized();
            if (model == null || !ModelState.IsValid) return BadRequest();
            if (model.SiteId != id) return BadRequest();
            bool wasSuccessful = await _siteService.UpdateSiteAsync(model);
            if (wasSuccessful) return Ok();
            return BadRequest();
        }

        [HttpPut("active/{id}")]
        public async Task<IActionResult> ToggleActive(int id)
        {
            if (!SetUserIdInService()) return Unauthorized();
            if (id == default || !ModelState.IsValid) return BadRequest();
            bool wasSuccessful = await _siteService.ToggleSiteActiveAsync(id);
            if (wasSuccessful) return Ok();
            return BadRequest();
        }

        [HttpDelete("delete/{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            if (!SetUserIdInService()) return Unauthorized();
            var site = await _siteService.GetSiteByIdAsync(id);
            if (site == null) return NotFound();
            bool wasSuccessful = await _siteService.DeleteSiteAsync(id);
            if (!wasSuccessful) return BadRequest();
            return Ok();
        }
    }
}
