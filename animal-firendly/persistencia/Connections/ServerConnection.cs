using Persistencia.Models;
using System;
using System.Collections.Generic;
using System.Net;
using System.IO;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

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
        /// <returns>Resposta del servidor en text pla (raw)</returns>
        string SendRequest(string message);
        /// <summary>
        /// Métode asíncron per enviar un missatge al servidor i rebre la seva resposta (raw)
        /// </summary>
        /// <param name="message">Missatge/comanda de text que s'envia al servidor</param>
        /// <returns>Resposta del servidor en text pla (raw), asíncron</returns>
        Task<string> SendRequestAsync(string message);

        /// <summary>
        /// Avança el lector una linia. Fet per solucionar alguns bugs del server quan ens torna més d'una linia per comanda;
        /// </summary>
        void LectorAvancaLinia();
    }

    public class ServerConnection : IServerConnection
    {
        private readonly string ipServidor;
        private readonly int portServidor;
        private TcpClient client;
        private StreamReader reader;
        private Stream stream;

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

        public string SendRequest(string message)
        {
            try
            {
                byte[] data = Encoding.UTF8.GetBytes(message + "\n");
                stream.Write(data, 0, data.Length);
                return reader.ReadLine();
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
            reader.Dispose();
            stream.Dispose();
            client.Dispose();
        }

        public void LectorAvancaLinia()
        {
           reader.ReadLine();           
        }
    }
}
