using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace Client
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Listening at 7777 (default) port\n\n");

            string accepted = Networker.Networker.AcceptText();

            var acceptedDeserialized = JsonConvert.DeserializeObject<List<DirList.DirListNode>>(accepted);

            foreach (var node in acceptedDeserialized)
            {
                Console.WriteLine($"{node.Name}, {{{node.Hash}, {node.Size}}}");
            }
        }
    }
}
