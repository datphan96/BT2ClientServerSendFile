using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace Client
{
    class Program
    {
        private const int BUFFER_SIZE = 1024;
        static ASCIIEncoding encoding = new ASCIIEncoding();
        static void Main(string[] args)
        {
            try
            {
                TcpClient client = new TcpClient();

                // 1. connect
                client.Connect("192.168.0.10", 12345);
                Stream stream = client.GetStream();

                Console.WriteLine("Connected to Server.");

                while (true)
                {
                    Console.Write("Enter your name: ");

                    string str = Console.ReadLine();
                    var reader = new StreamReader(stream);
                    var writer = new StreamWriter(stream);
                    writer.AutoFlush = true;

                // 2. send
                writer.WriteLine(str);

                // 3. receive
                str = reader.ReadLine();
                Console.WriteLine(str);
                if (str.ToUpper() == "BYE")
                break;
                }

                // 4. Close
                stream.Close();
                client.Close();
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error: " + ex);
            }

            Console.ReadLine();
        }
    }
}
