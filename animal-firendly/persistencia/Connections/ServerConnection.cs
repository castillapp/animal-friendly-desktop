using Persistencia.Models;
using System;
using System.Collections.Generic;
using System.Net;
using System.IO;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using System.Net.Security;
using System.Security.Cryptography.X509Certificates;

namespace Persistencia.Connections
{
    /// <summary>
    /// Classe que gestiona la connexió i envia peticions i rep respostes.
    /// Implementa IDisposable, quan es fa un "dispose" de la classe, aquesta ja tancará la connexió
    /// si aquesta está oberta.
    /// </summary>
    public interface IServerConnection : IDisposable
    {
        ///// <summary>
        ///// Connecta amb la BBDD
        ///// </summary>
        //void Connect();
        /// <summary>
        /// Desconnecta de la BBDD
        /// </summary>
        void Disconnect();
        /// <summary>
        /// Envia un missatge al servidor i rep la seva resposta (raw)
        /// </summary>
        /// <param name="message">Missatge/comanda de text que s'envia al servidor</param>
        /// <param name="checkOperacio">Comprova que la linia que es llegeix pertanyia a la comanda especificada, sino, avança linies fins trobar-ho</param>
        /// <returns>Resposta del servidor en text pla (raw)</returns>
        string SendRequest(string message, string checkOperacio = null);
        /// <summary>
        /// Métode asíncron per enviar un missatge al servidor i rebre la seva resposta (raw)
        /// </summary>
        /// <param name="message">Missatge/comanda de text que s'envia al servidor</param>
        /// <returns>Resposta del servidor en text pla (raw), asíncron</returns>
        Task<string> SendRequestAsync(string message);

    }

    public class ServerConnection : IServerConnection
    {
        private readonly string ipServidor;
        private readonly int portServidor;
        private TcpClient client;
        private StreamReader reader;
        private Stream stream;
        private SslStream sslStream;

        public ServerConnection(string ipServidor, int portServidor)
        {
            this.ipServidor = ipServidor;
            this.portServidor = portServidor;
            Connect();
        }

        private void Connect()
        {
            try
            {
                client = new TcpClient(ipServidor, portServidor);
                stream = client.GetStream();
                reader = new StreamReader(stream);
            }
            catch (Exception)
            {
                throw;
            }
        }

        private void ConnectWithSsl()
        {
            try
            {
                client = new TcpClient(ipServidor, portServidor);
                sslStream = new SslStream(client.GetStream(), false,
                    new RemoteCertificateValidationCallback(ValidateServerCertificate), null);
                sslStream.AuthenticateAsClient(ipServidor);

                stream = client.GetStream();
                reader = new StreamReader(stream);
            }
            catch (Exception)
            {

                throw;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="certificate"></param>
        /// <param name="chain"></param>
        /// <param name="sslPolicyErrors"></param>
        /// <returns></returns>
        private static bool ValidateServerCertificate(object sender, X509Certificate certificate,
            X509Chain chain, SslPolicyErrors sslPolicyErrors)
        {
            return true;
        }

        public async Task<string> SendRequestAsync(string message)
        {
            try
            {
                byte[] data = Encoding.UTF8.GetBytes(message+"\n");
                stream.Write(data, 0, data.Length);
                return await reader.ReadLineAsync();
            }
            catch (Exception)
            {
                throw;
            }
        }

        public string SendRequest(string message, string checkOperacio = null)
        {
            try
            {
                byte[] data = Encoding.UTF8.GetBytes(message + "\n");
                stream.Write(data, 0, data.Length);
                var res = reader.ReadLine();
                if(checkOperacio != null)
                {
                    while (!res.Contains(checkOperacio))
                    {
                        res = reader.ReadLine();
                    }
                }
                return res;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public void Disconnect()
        {
            //X es la comanda per avisar al servidor que es tanca la connexio
            SendRequest("X");
            client.Close();
        }

        public void Dispose()
        {
            if (client.Connected)
            {
                Disconnect();
            }
            if(sslStream != null)
            {
                sslStream.Dispose();
            }
            reader.Dispose();
            stream.Dispose();
            client.Dispose();
        }
    }
}
