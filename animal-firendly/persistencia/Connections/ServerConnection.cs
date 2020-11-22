using Persistencia.Models;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using System.Text;

namespace Persistencia.Connections
{

    public interface IServerConnection : IDisposable
    {

    }

    public class ServerConnection : IServerConnection
    {
        private readonly IPEndPoint direccioServidor;
        private readonly Socket socket;
        private readonly IRequestEncoder encoder;
        private readonly int bufferSize;

        public ServerConnection(IRequestEncoder encoder, string ipServidor, string portServidor, int bufferSize)
        {
 
            var ipAddress = IPAddress.Parse(ipServidor);
            direccioServidor = new IPEndPoint(ipAddress, Int32.Parse(portServidor));

            try
            {
                socket = new Socket(ipAddress.AddressFamily, SocketType.Stream, ProtocolType.Tcp);
            }
            catch (Exception)
            {

                throw;
            }

            this.encoder = encoder;
            this.bufferSize = bufferSize;
        }

        public void Connect()
        {
            socket.Connect(direccioServidor);
        }

        public T Query<T>(string message) where T : BaseModel
        {
            socket.Send(encoder.EncodeRequest(message));

            var buffer = new byte[bufferSize];

            int bytesReceived = socket.Receive(buffer);

            var result = encoder.DecodeResponse<T>(buffer);

            return result;                
        }

        public void Disconnect()
        {
            socket.Send(encoder.EncodeRequest("X"));
            socket.Shutdown(SocketShutdown.Both);
            socket.Close();
        }

        public void Dispose()
        {
            if (socket.Connected)
            {
                Disconnect();
            }
            socket.Dispose();
        }
    }
}
