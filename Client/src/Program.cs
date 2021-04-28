using System;

namespace Client
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Listening at 7777 (default) port\n\n");

            string accepted = Networker.Networker.AcceptText();

            Console.WriteLine(accepted);
        }
    }
}
