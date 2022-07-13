using SteamPlayerStatsApp.Models;
using System.Threading.Tasks;
using System.Net.Http;
using System.Text.Json;

namespace SteamPlayerStatsApp.Helpers
{
    public class SteamHelper
    {
        private static HttpClient _client = new HttpClient();
        private static SteamData _currentData;
        private static string _currentSteamID;

        public static async Task<dynamic> RetrieveSteamData(string steamKey, string steamId)
        {
            _currentSteamID = steamId;
            try
            {
                var url = String.Format("http://api.steampowered.com/IPlayerService/GetOwnedGames/v0001/?key={0}&steamid={1}&format=json", steamKey, steamId);
                HttpResponseMessage response = await _client.GetAsync(url);
                response.EnsureSuccessStatusCode();
                string responseBody = await response.Content.ReadAsStringAsync();

                SteamData data = JsonSerializer.Deserialize<SteamData>(responseBody);

                _currentData = data;

                return data;

            }
            catch (Exception e)
            {
                return String.Format("\nPlease check your SteamID is correct, and your profile is public \nMessage :{0} ", e.Message);
            }

        }

        public static int RetrieveTotalPlaytime(string steamKey, string steamID) 
        {
            int totalPlayTime = 0;
            SteamData data;
            if (steamID != _currentSteamID)
            {
                data = RetrieveSteamData(steamKey, steamID).Result;
            }
            else
            {
                data = _currentData;
            }

            Game[] games = data.response.games;
            foreach (Game game in games)
            {
                totalPlayTime += game.playtime_forever;
            }
            return totalPlayTime;
        }

        public static int RetrieveTotalGames(string steamKey, string steamID)
        {
            SteamData data;
            if (steamID != _currentSteamID)
            {
                data = RetrieveSteamData(steamKey, steamID).Result;
            }
            else
            {
                data = _currentData;
            }

            return data.response.game_count;
        }
    }
}
