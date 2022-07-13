using GamerStatsApp.Models;
using System.Threading.Tasks;
using System.Net.Http;
using System.Text.Json;

namespace GamerStatsApp.Helpers
{
    public class SteamHelper
    {
        private static HttpClient client = new HttpClient();
        private static SteamData currentData;
        private static string currentSteamId;

        public static async Task<dynamic> RetrieveSteamData(string steamKey, string steamId)
        {
            currentSteamId = steamId;
            try
            {
                var url = String.Format("http://api.steampowered.com/IPlayerService/GetOwnedGames/v0001/?key={0}&steamid={1}&format=json", steamKey, steamId);
                HttpResponseMessage response = await client.GetAsync(url);
                response.EnsureSuccessStatusCode();
                string responseBody = await response.Content.ReadAsStringAsync();

                SteamData data = JsonSerializer.Deserialize<SteamData>(responseBody);

                currentData = data;

                return data;

            }
            catch (Exception e)
            {
                return String.Format("\nException Caught! \nMessage :{0} ", e.Message);
            }

        }

        public static int GetTotalPlaytime(string steamKey, string steamId) 
        {
            int totalPlayTime = 0;
            SteamData data;
            if (steamId != currentSteamId)
            {
                data = RetrieveSteamData(steamKey, steamId).Result;
            }
            else
            {
                data = currentData;
            }

            Game[] games = data.response.games;
            foreach (Game game in games)
            {
                totalPlayTime += game.playtime_forever;
            }
            return totalPlayTime;
        }
    }
}
