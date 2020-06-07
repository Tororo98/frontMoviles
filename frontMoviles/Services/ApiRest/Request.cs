using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using frontMoviles.Models;

namespace frontMoviles.Services.ApiRest
{
    public abstract class Request<T>
    {
        #region properties
        protected string Url { get; set; }
        protected string Verb { get; set; }

        private static HeaderService headerService;

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
        #endregion properties

        #region gets & sets

        #endregion gets & sets

        #region methods

        #endregion methods 
    }
}
