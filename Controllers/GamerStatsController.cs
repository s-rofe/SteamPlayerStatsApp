using GamerStatsApp.Helpers;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace GamerStatsApp.Controllers
{
    [Route("api")]
    [ApiController]
    public class GamerStatsController : Controller
    {
        private readonly KeyReader KeyReader;
        private readonly SteamHelper SteamHelper;

        public GamerStatsController()
        {
            KeyReader = new KeyReader();
            SteamHelper = new SteamHelper();
        }

        // Get the current version of the backend
        [HttpGet("GetVersion")]
        public ActionResult GetVersion() => Ok("v 0.1.0");


        // Get all Steam game and playtime data for a Steam user
        // Returns a JSON reprosentation
        [HttpGet("GetSteamData/steam_id")]

        public dynamic GetSteamData(string steamId)
        {
            string steamKey = KeyReader.ReadSteamKey();
            if (steamKey.Contains("Error"))
            {
                ContentResult r = new ContentResult
                {
                    Content = String.Format("Whoops there was a problem: {0}.", steamKey),
                    ContentType = "text/plain;  charset=utf-8",
                    StatusCode = (int)HttpStatusCode.InternalServerError,
                };
                return r;
            }

            // Try and request the data from the Steam API
            try
            {

                return Ok(SteamHelper.RetrieveSteamData(steamKey, steamId).Result);


            }
            catch (HttpRequestException e)
            {
                return e;
            }

        }
    }
}
