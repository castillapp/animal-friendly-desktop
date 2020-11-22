using Newtonsoft.Json;
using Persistencia.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Persistencia.Connections
{

    /// <summary>
    /// Interficie de la classe que codifica i descodifica els missatges que s'envien a la BBDD
    /// </summary>
    public interface IRequestEncoder
    {
        /// <summary>
        /// Converteix un missatge de text en un array de bytes interpretable pel Socket del servidor
        /// </summary>
        /// <param name="request">missatge de text</param>
        /// <returns>missatge en bytes</returns>
        byte[] EncodeRequest(string request);

        /// <summary>
        /// Decodifica un missatge en bytes del Socket del servidor
        /// </summary>
        /// <param name="response">missatge en bytes</param>
        /// <returns>resposta en text</returns>
        string DecodeResponse(byte[] response);

        /// <summary>
        /// Decodifica un missatge en bytes del Servidor en un model de dades concret
        /// </summary>
        /// <typeparam name="T">Classe del model de dades</typeparam>
        /// <param name="response">missatge en bytes</param>
        /// <returns>Objecte de la resposta</returns>
        T DecodeResponse<T>(byte[] response) where T : BaseModel;
    }

    public class RequestEncoder : IRequestEncoder
    {
        public string DecodeResponse(byte[] response)
        {
            var returnMsg = Encoding.UTF8.GetString(response, 0, response.Length);
            return returnMsg;
        }

        public T DecodeResponse<T>(byte[] response) where T : BaseModel
        {
            var jsonString = DecodeResponse(response);
            return JsonConvert.DeserializeObject<T>(jsonString);
        }

        public byte[] EncodeRequest(string request)
        {
            byte[] msg = Encoding.UTF8.GetBytes(request + "\n");
            return msg;
        }
    }
}
