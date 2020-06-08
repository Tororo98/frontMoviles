using System;
using System.Collections.Generic;
using System.Text;

namespace frontMoviles.Configuration
{
    public class ConfigurationRest
    {
        #region Properties
        private readonly string NameSpaceRest;
        public Dictionary<string, string> VerbsConfiguration { get; set; }
        #endregion Properties

        #region Initialize
        public ConfigurationRest()
        {
            NameSpaceRest = "frontMoviles.Services.ApiRest.";
            InitializeVerbsConfiguration();

        }
        #endregion Initialize

        #region Métodos
        private void InitializeVerbsConfiguration()
        {
            VerbsConfiguration = new Dictionary<string, string>();
            VerbsConfiguration.Add("GET", string.Concat(NameSpaceRest, "RequestParameters`1"));
            VerbsConfiguration.Add("DELETE", string.Concat(NameSpaceRest, "RequestParameters`1"));
            VerbsConfiguration.Add("POST", string.Concat(NameSpaceRest, "RequestBody`1"));
            VerbsConfiguration.Add("PUT", string.Concat(NameSpaceRest, "RequestBody`1"));
        }

        #endregion Métodos 
    }
}
