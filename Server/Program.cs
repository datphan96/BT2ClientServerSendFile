using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace Server
{
    class Program
    {
        private const int BUFFER_SIZE = 1024;
        private const int PORT_NUMBER = 12345;
        static ASCIIEncoding encoding = new ASCIIEncoding();
        static void Main(string[] args)
        {
            try
            {
                IPAddress address = IPAddress.Parse("192.168.0.10");

                TcpListener listener = new TcpListener(address, PORT_NUMBER);

                // 1. listen
                listener.Start();

                Console.WriteLine("Server started on " + listener.LocalEndpoint);
                Console.WriteLine("Waiting for a connection...");

                Socket socket = listener.AcceptSocket();
                Console.WriteLine("Connection received from " + socket.RemoteEndPoint);

                var stream = new NetworkStream(socket);
                var reader = new StreamReader(stream);
                var writer = new StreamWriter(stream);

                while (true)
                {
                    // 2. receive
                    string str = reader.ReadLine();
                    if (str.ToUpper() == "EXIT")
                    {
                        writer.WriteLine("Bye");
                        break;
                    }
                    // 3. send
                    writer.WriteLine("Hello " + str);
                }
              
                    // 4. close
                   stream.Close();
                   socket.Close();
                   listener.Stop();


            }

            catch (Exception ex)

            {
                Console.WriteLine("Error: " + ex);
            }

            Console.ReadLine();

        }
    }
}
