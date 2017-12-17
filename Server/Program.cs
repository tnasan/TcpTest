using System;
using System.Diagnostics;
using System.Net.Sockets;
using System.Text;
using System.Threading;

namespace Server
{
    class Program
    {
        static void Main(string[] args)
        {
            TcpListener listen = new TcpListener(System.Net.IPAddress.Loopback, 500);
            listen.Start();

            byte[] buffer = new byte[65535];
            while (true)
            {
                var client = listen.AcceptTcpClient();
                var stream = client.GetStream();

                int readLength;
                while ((readLength = stream.Read(buffer, 0, buffer.Length)) != 0)
                {
                    //Thread.Sleep(300);
                    var data = Encoding.ASCII.GetString(buffer, 0, readLength);
                    Debug.WriteLine("Server: " + data);
                    if (data.CompareTo("Hello World") == 0)
                    {
                        var dataToSend = Encoding.ASCII.GetBytes("Hello Too!");
                        stream.Write(dataToSend, 0, dataToSend.Length);
                    }
                }

                client.Close();
            }
        }
    }
}
