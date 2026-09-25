using Microsoft.AspNetCore.Mvc;
using Server.Entities;
using Server.Services;

namespace Server.Controllers
{
    [Route("api/[controller]")]
    public class MapController(ILogger<MapController> logger) : Controller
    {
        [HttpGet]
        public IActionResult GetMap()
        {
            List<Cell> maps = new();
            ReloadMap.Map(maps);
            logger.LogInformation("GetMap controller");
            return Ok(maps);
        }
    }
}
