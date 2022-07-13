using GamerStatsApp.Helpers;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace GamerStatsApp.Controllers
{
    [Route("api")]
    [ApiController]
    public class GamerStatsController : Controller
    {
        private readonly KeyReader _keyReader;
        private readonly SteamHelper _steamHelper;

        public GamerStatsController()
        {
            _keyReader = new KeyReader();
            _steamHelper = new SteamHelper();
        }

        // Get the current version of the backend
        [HttpGet("GetVersion")]
        public ActionResult GetVersion() => Ok("v 0.1.0");


        // Get all Steam game and playtime data for a Steam user in minutes
        // Returns a JSON reprosentation
        [HttpGet("GetSteamData/{steam_id}")]
        public dynamic GetSteamData(string steamID)
        {
            string steamKey = _keyReader.ReadSteamKey();
            if (steamKey.Contains("Error"))
            {
                return CreateErrorResponse(steamID);
            }

            // Try and request the data from the Steam API
            try
            {
                return Ok(SteamHelper.RetrieveSteamData(steamKey, steamID).Result);
            }
            catch (HttpRequestException e)
            {
                return CreateErrorResponse(e.Message);
            }

        }


        // Get the total playtime number across all games for the user
        // Returns the playtime as an int if successful, else returns error message as ContentResult
        [HttpGet("GetTotalPlayTime/{steamID}")]
        public ActionResult GetTotalPlayTime(string steamID)
        {
            string steamKey = _keyReader.ReadSteamKey();
            if (steamKey.Contains("Error"))
            {
                return CreateErrorResponse(steamID);
            }

            // Try and retrieve the total play-time, calculated by the helper method, which will call the Steam API
            // unless the last API call to this server was for the same user.
            try
            {
                return Ok(SteamHelper.RetrieveTotalPlaytime(steamKey, steamID));
            }
            catch (HttpRequestException e)
            {
                return CreateErrorResponse(e.Message);
            }
        }

        [HttpGet("GetTotalGames/{steamID}")]
        public ActionResult GetTotalGames(string steamID)
        {
            string steamKey = _keyReader.ReadSteamKey();
            if (steamKey.Contains("Error"))
            {
                return CreateErrorResponse(steamKey);
            }

            // Try and retrieve the total games owned by the user (including free games),
            // calculated by the helper method, which will call the Steam API
            // unless the last API call to this server was for the same user.
            try
            {
                return Ok(SteamHelper.RetrieveTotalGames(steamKey, steamID));
            }
            catch (HttpRequestException e)
            {
                return CreateErrorResponse(e.Message);
            }
        }

        // Creates a ContentResult which returns the error message in question and HTTP status code 500
        private ContentResult CreateErrorResponse(string errorMessage)
        {
            ContentResult r = new ContentResult
            {
                Content = String.Format("Whoops there was a problem: {0}.", errorMessage),
                ContentType = "text/plain;  charset=utf-8",
                StatusCode = (int)HttpStatusCode.InternalServerError,
            };
            return r;
        }

    }
}
