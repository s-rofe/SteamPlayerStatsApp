namespace GamerStatsApp.Helpers
{
    public class SteamHelper
    {
        private static readonly HttpClient client = new HttpClient();

        public static async Task<string> RetrieveSteamData(string steamKey)
        {
            try
            {
                var steam_id = "76561198093074081";
                var url = String.Format("http://api.steampowered.com/IPlayerService/GetOwnedGames/v0001/?key={0}&steamid={1}&format=json", steamKey, steam_id);
                HttpResponseMessage response = await client.GetAsync(url);
                response.EnsureSuccessStatusCode();
                string responseBody = await response.Content.ReadAsStringAsync();

                return responseBody;
            }
            catch (HttpRequestException e)
            {
                return String.Format("\nException Caught! \nMessage :{0} ", e.Message);
            }
        }
    }
}
