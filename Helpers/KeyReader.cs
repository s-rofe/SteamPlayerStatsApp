namespace GamerStatsApp.Helpers
{
    public class KeyReader
    {
        private string steamKey { get; set; }

        public void ReadSteamKey()
        {
            string workingDirectory = Environment.CurrentDirectory;
            string projectDirectory = Directory.GetParent(workingDirectory).Parent.Parent.FullName;

            string keyFilePath = String.Format(@"{0}\Keys.txt", projectDirectory);
            string[] lines = System.IO.File.ReadAllLines(keyFilePath);
            int keyStartIndex = lines[0].IndexOf(": ");

            steamKey = lines[0].Substring(keyStartIndex + 2);
        }
    }
}
