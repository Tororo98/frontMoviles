using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;

namespace frontMoviles.Services
{
    public class HeaderService
    {
        #region attributes
        public Dictionary <string, string> Headers { get; set; }
        #endregion attributes

        #region Initialize
        public HeaderService()
        {
            Headers = new Dictionary<string, string>();
            Headers.Add("ContentType", "application/json"); //Useful when making login and api token is
        }                                                   //needed. If exists then send it, otherwise dont
        #endregion Initialize

        #region methods
        public HttpRequestMessage AddHeaders(HttpRequestMessage requestMessage)
        {
            foreach(var h in Headers)
            {
                requestMessage.Headers.Add(h.Key, h.Value);
            }
            return requestMessage;
        }
        #endregion methods
    }
}
