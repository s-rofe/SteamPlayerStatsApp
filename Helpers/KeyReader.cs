namespace SteamPlayerStatsApp.Helpers
{
    public class KeyReader
    {

        public string ReadSteamKey()
        {
            string projectDirectory = Directory.GetCurrentDirectory();
            string keyFilePath = String.Format(@"{0}\Keys.txt", projectDirectory);

            // Try to read in the key from Key.txt and return it
            try
            {
                string[] lines = System.IO.File.ReadAllLines(keyFilePath);
                int keyStartIndex = lines[0].IndexOf(": ");
                return lines[0].Substring(keyStartIndex + 2);
            }
            // If the file is not found, or another error occurs, return a string explaining the error
            catch (FileNotFoundException e)
            {
                return "Error: Key file does not exist, please ensure Key.txt is set up as specified in README.md";
            }
            catch (Exception e) 
            {
                return String.Format("\nError: \nMessage :{0} ", e.Message);
            }
        }
    }
}
