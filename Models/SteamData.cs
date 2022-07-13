namespace SteamPlayerStatsApp.Models
{

    public class SteamData
    {
        public Response response { get; set; }
    }

    public class Response
    {
        public int game_count { get; set; }
        public Game[] games { get; set; }
    }

    public class Game
    {
        public int appid { get; set; }
        public int playtime_forever { get; set; }
        public int playtime_windows_forever { get; set; }
        public int playtime_mac_forever { get; set; }
        public int playtime_linux_forever { get; set; }
        public int playtime_2weeks { get; set; }
    }



}
