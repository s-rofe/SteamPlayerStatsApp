using Microsoft.AspNetCore.Mvc;
using GamerStatsApp.Helpers;

namespace GamerStatsApp.Controllers
{
    [Route("api")]
    [ApiController]
    public class GamerStatsController : Controller
    {

        [HttpGet("hello")]
        public ActionResult hello() => Ok("Hello");
   
        [HttpGet("GetVersion")]
        public ActionResult GetVersion() => Ok("v 0.1.0");
    }
}
