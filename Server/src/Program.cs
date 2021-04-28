using Newtonsoft.Json;

namespace Server
{
    class Program
    {
        static void Main(string[] args)
        {
            const string path = ".";
            string json;

            DirList.DirList dirList = new(path);
            json = JsonConvert.SerializeObject(dirList.GetNodesToSend());

            dirList.Visualize();

            try
            {
                Networker.Networker.SendText(json);
                System.Console.WriteLine("\n\n\n\nSuccessfully sent JSON to the client, you can check it now\n\n");
            }
            catch
            {
                System.Console.WriteLine("\n\n\n\nYou can also send JSON of this dir to the client project, so please launch it before start server again to get it work\n\n");
            }
        }
    }
}
