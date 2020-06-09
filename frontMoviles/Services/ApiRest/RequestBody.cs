using System;
using System.Collections.Generic;
using System.Text;
using System.Net.Http;
using System.Security.Cryptography.X509Certificates;
using System.Threading.Tasks;
using frontMoviles.Models.AuxiliarModels;
using Newtonsoft.Json;


namespace frontMoviles.Services.ApiRest
{
    public class RequestBody<T> : Request<T>
    {
        public RequestBody(string url, string verb)
        {
            Url = url;
            Verb = verb;
        }

        public override async Task<APIResponse> SendRequest(T objecto)
        {
            /*Verbos POST y PUT*/

            APIResponse respuesta = new APIResponse()
            {
                Code = 400,
                IsSuccess = false,
                Response = ""
            };

            string objetoJson = JsonConvert.SerializeObject(objecto);            
            HttpContent content = new StringContent(objetoJson, Encoding.UTF8, "application/json");

            try
            {
                using (var client = new HttpClient())
                {
                    var verbHttp = (Verb == "POST") ? HttpMethod.Post : HttpMethod.Put;
                    HttpRequestMessage requestMessage = new HttpRequestMessage(verbHttp, UrlParameters);
                    //requestMessage = ServicioHeaders.AgregarCabeceras(requestMessage);
                    requestMessage.Content = content;
                    client.Timeout = TimeSpan.FromSeconds(50);
                    HttpResponseMessage HttpResponse = client.SendAsync(requestMessage).Result;
                    respuesta.Code = Convert.ToInt32(HttpResponse.StatusCode);
                    respuesta.IsSuccess = HttpResponse.IsSuccessStatusCode;
                    respuesta.Response = await HttpResponse.Content.ReadAsStringAsync();
                }
            }
            catch (Exception)
            {
                respuesta.Response = "Error al momento de llamar al servidor";
            }

            return respuesta;
        }
    }
}
