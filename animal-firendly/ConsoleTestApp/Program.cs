using System;
using System.Net;
using System.Net.Sockets;
using System.Text;

namespace ConsoleTestApp
{
    class Program
    {
        static void Main(string[] args)
        {
            Run();
        }

        public static void Run()
        {
            byte[] buffer = new byte[1024];

            try
            {
                IPAddress ipAddress = IPAddress.Parse("192.168.2.199");
                IPEndPoint remoteEP = new IPEndPoint(ipAddress, 9900);

                Socket sender = new Socket(ipAddress.AddressFamily, SocketType.Stream, ProtocolType.Tcp);

                try
                {
                    sender.Connect(remoteEP);

                    //escribim el missatge

                    byte[] msg = Encoding.UTF8.GetBytes("login:11111111A:admin0\n");
                    int bytesSent = sender.Send(msg);

                    int bytesRec = sender.Receive(buffer);
                    var returnMsg = Encoding.UTF8.GetString(buffer, 0, bytesRec);

                    msg = Encoding.UTF8.GetBytes("X\n");
                    bytesSent = sender.Send(msg);

                    sender.Shutdown(SocketShutdown.Both);
                    sender.Close();

                }
                catch (Exception)
                {

                    throw;
                }

            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
