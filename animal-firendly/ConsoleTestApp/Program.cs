using System;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.IO;
using System.Linq;
using System.Net.Security;
using System.Security.Cryptography.X509Certificates;

namespace ConsoleTestApp
{

    abstract class A
    {
        static public int MyProperty { get; set; }
    }

    class B : A
    {
        
    }

    class C : A
    {

    }

    class Program
    {
        static void Main(string[] args)
        {
            Tcp();
        }

        public static bool ValidateServerCertificate(object sender, X509Certificate certificate,
            X509Chain chain, SslPolicyErrors sslPolicyErrors)
        {
            return true;
        }

        public static void Tcp()
        {
            byte[] buffer = new byte[1024];

            try
            {
                Int32 port = 9900;
                //TcpClient client = new TcpClient("192.168.2.200", port);
                string server = "170.253.52.113";
                server = "192.168.2.200";
                TcpClient client = new TcpClient(server, port);
                NetworkStream stream = client.GetStream();

                //using (SslStream sslStream = new SslStream(client.GetStream(), false,
                //    new RemoteCertificateValidationCallback(ValidateServerCertificate), null))
                //{
                //    sslStream.AuthenticateAsClient(server);
                //    // This is where you read and send data
                //}

                var reader = new StreamReader(stream);

                byte[] data = Encoding.UTF8.GetBytes("login:11111111A:admin0\n");
                stream.Write(data, 0, data.Length);
                var a = reader.ReadLine();

                data = Encoding.UTF8.GetBytes("updzon:4:13:222\n");
                stream.Write(data, 0, data.Length);

                var res = reader.ReadLine();


                var res2 = System.Text.RegularExpressions.Regex.Match(res, @"(?<=\[)(.*?)(?=\])").Value;
                var llistatCamps = res2.Substring(1, res2.Length - 2);
                var camps = System.Text.RegularExpressions.Regex.Split(llistatCamps, @"""\s*,\s*""").Select(f=> f.Split(":").Select(y=>y.Trim()));
                //String responseData = String.Empty;
                //Int32 bytes = stream.Read(data, 0, data.Length);
                //responseData = System.Text.Encoding.UTF8.GetString(data, 0, bytes);
            }
            catch (Exception)
            {

                throw;
            }
        }



        public static void Run()
        {
            byte[] buffer = new byte[1024];

            try
            {
                IPAddress ipAddress = IPAddress.Parse("192.168.2.200");
                //IPAddress ipAddress = IPAddress.Parse("170.253.52.113");

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

                    //select 
                    //msg = Encoding.UTF8.GetBytes("seltre:11111111A\n");
                    msg = Encoding.UTF8.GetBytes("selani:x\n");
                    bytesSent = sender.Send(msg);
                    bytesRec = sender.Receive(buffer);
                    returnMsg = Encoding.UTF8.GetString(buffer, 0, bytesRec);

                    //tanquem connexio

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
