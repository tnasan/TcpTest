using System;
using System.Diagnostics;
using System.Text;
using System.Threading;
using System.Net.Sockets;
using RestSharp;
using RestSharp.Authenticators;

namespace Client
{
    class Program
    {
        static void Main(string[] args)
        {
            TcpClient client = new TcpClient(System.Net.IPAddress.Loopback.ToString(), 500);
            var stream = client.GetStream();
            stream.ReadTimeout = 500;

            var sendText = "Hello World";
            var encodedText = Encoding.ASCII.GetBytes(sendText);
            stream.Write(encodedText, 0, encodedText.Length);

            var buffer = new byte[65535];
            do
            {
                try
                {
                    var readCount = stream.Read(buffer, 0, 65535);
                    Debug.WriteLine("Client: " + Encoding.ASCII.GetString(buffer, 0, readCount));
                }
                catch (Exception e)
                {
                    break;
                }
            } while (true);

            Console.WriteLine("Finish!");
            Console.WriteLine($"Connection: {client.Connected}");

            Console.ReadKey();
        }
    }
}
