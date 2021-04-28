using System;
using Newtonsoft.Json;

namespace Server
{
    class Program
    {
        static void Main(string[] args)
        {
            const string path = ".";

            DirList dirList = new(path);

            Console.WriteLine(JsonConvert.SerializeObject(dirList.GetNodesToSend()));
        }
    }
}
