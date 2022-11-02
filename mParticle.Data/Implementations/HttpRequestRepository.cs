

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Xml;
using Microsoft.Extensions.Logging;
using mParticle.Data.Definitions;
using mParticle.Domain.DTO;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace mParticle.Data.Implementations
{
    public class HttpRequestRepository : IHttpRequestRepository
    {
        public HttpRequestRepository()
        {
        }
        public async Task<HttpResponseDTO> RequestHTTP(ConfigRequestDTO configRequestDTO, HttpRequestDTO httpRequestDTO)
        {
            HttpResponseDTO httpResponseDTO = new HttpResponseDTO();
            try
            {
                string resultado = string.Empty;
                HttpWebRequest request = (HttpWebRequest)WebRequest.Create($"{configRequestDTO.ServerURL}");
                request.AutomaticDecompression = DecompressionMethods.GZip | DecompressionMethods.Deflate;
                request.Accept = "application/json";
                request.ContentType = "application/json";
                request.Headers.Add("X-Api-Key", configRequestDTO.AuthKey);
                request.Method = WebRequestMethods.Http.Post;

                using (var streamWriter = new StreamWriter(request.GetRequestStream()))
                {
                    string json = JsonConvert.SerializeObject(httpRequestDTO);

                    streamWriter.Write(json);
                }

                using (HttpWebResponse response = (HttpWebResponse)request.GetResponse())
                using (Stream stream = response.GetResponseStream())
                using (StreamReader reader = new StreamReader(stream))
                {
                    resultado = reader.ReadToEnd();
                }

                var responseObj = JObject.Parse(resultado);

                httpResponseDTO = JsonConvert.DeserializeObject<HttpResponseDTO>(JsonConvert.SerializeObject(responseObj));

                //_logger.LogInformation($"Current Request: {httpRequestDTO.requests_sent} Result: {httpResponseDTO.successful}");
            }
            catch (Exception ex)
            {
                httpResponseDTO.isError = true;
                httpResponseDTO.error = ex.Message;
            }

            return httpResponseDTO;
        }
        
    }
}