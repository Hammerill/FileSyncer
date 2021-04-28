using System.Net;
using System.Net.Sockets;
using System.Text;

namespace Networker
{
    public static class Networker
    {
        public static void SendText(string data, string ip = "127.0.0.1", int port = 7777)
        {
            Socket connectTo = new(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);

            IPEndPoint endPoint = new(IPAddress.Parse(ip), port);
            connectTo.Connect(endPoint);

            connectTo.Send(Encoding.UTF8.GetBytes(data));

            connectTo.Shutdown(SocketShutdown.Both);
            connectTo.Close();
        }
        public static string AcceptText(int port = 7777)
        {
            IPEndPoint ipEndPoint = new(IPAddress.Any, port);

            Socket listener = new(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            Socket sender;

            listener.Bind(ipEndPoint);
            listener.Listen();

            sender = listener.Accept();

            int bytes;
            byte[] bytesReceived = new byte[1024];
            string data;

            bytes = sender.Receive(bytesReceived, bytesReceived.Length, 0);
            data = Encoding.UTF8.GetString(bytesReceived, 0, bytes);

            while (bytes > 0)
            {
                bytes = sender.Receive(bytesReceived, bytesReceived.Length, 0);
                data += Encoding.UTF8.GetString(bytesReceived, 0, bytes);
            }

            sender.Shutdown(SocketShutdown.Both);
            sender.Close();

            return data;
        }
    }
}
