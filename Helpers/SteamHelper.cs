using GamerStatsApp.Models;
using System.Threading.Tasks;
using System.Net.Http;
using System.Text.Json;

namespace GamerStatsApp.Helpers
{
    public class SteamHelper
    {
        private static HttpClient client = new HttpClient();

        public static async Task<dynamic> RetrieveSteamData(string steamKey, string steamId)
        {
            try
            {
                var url = String.Format("http://api.steampowered.com/IPlayerService/GetOwnedGames/v0001/?key={0}&steamid={1}&format=json", steamKey, steamId);
                HttpResponseMessage response = await client.GetAsync(url);
                response.EnsureSuccessStatusCode();
                string responseBody = await response.Content.ReadAsStringAsync();

                dynamic data = JsonSerializer.Deserialize<dynamic>(responseBody);

                return data;

            }
            catch (Exception e)
            {
                return String.Format("\nException Caught! \nMessage :{0} ", e.Message);
            }

        }
    }
}
