using System;
using System.Collections.Generic;
using System.Text;
using System.Net.Http;
using System.Threading.Tasks;
using frontMoviles.Models.AuxiliarModels;

namespace frontMoviles.Services.ApiRest
{
    public abstract class Request<T>
    {
        #region properties
        protected string Url { get; set; }
        protected string Verb { get; set; }
        protected string UrlParameters { get; set; }

        private static HeaderService headerService;

        #endregion properties

        #region gets & sets
        protected static HeaderService HeaderService
        {
            get
            {
                if (headerService == null)
                {
                    headerService = new HeaderService();
                }
                return headerService;
            }
        }
        #endregion gets & sets

        #region methods
        public abstract Task<APIResponse> SendRequest(T objecto);

        public async Task ContruirURL(ParametersRequest parametros)
        {
            ParametersRequest Parametros = parametros as ParametersRequest;
            string newURL = Url;

            if (Parametros.Parameters.Count > 0)
            {
                newURL = (newURL.Substring(Url.Length - 1) == "/") ? newURL.Remove(newURL.Length - 1) : newURL;
                Parametros.Parameters.ForEach(p => newURL += "/" + p);
            }

            if (Parametros.QueryParameters.Count > 0)
            {
                var queryParameters = await new FormUrlEncodedContent(Parametros.QueryParameters).ReadAsStringAsync();
                newURL += queryParameters;
            }

            UrlParameters = newURL;
        }


        #endregion methods 
    }
}
