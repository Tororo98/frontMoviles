using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using frontMoviles.Configuration;
using frontMoviles.Models.AuxiliarModels;

namespace frontMoviles.Services.ApiRest
{
    public class ChooseRequest<T>
    {
        #region Properties
        public Request<T> EstrategiaEnvio { get; set; }
        public ConfigurationRest ConfigurationRest { get; set; }
        #endregion Properties

        #region Initialize
        public ChooseRequest()
        {
            ConfigurationRest = new ConfigurationRest();
        }
        #endregion Initialize

        #region Métodos
        public void ElegirEstrategia(string verb, string url)
        {
            var diccionario = ConfigurationRest.VerbsConfiguration;
            string nombreClase;
            if (diccionario.TryGetValue(verb.ToUpper(), out nombreClase))
            {
                Type tipoClase = Type.GetType(nombreClase);
                Type[] typeArgs = { typeof(T) };
                var genericClass = tipoClase.MakeGenericType(typeArgs);
                EstrategiaEnvio = (Request<T>)Activator.CreateInstance(genericClass, url, verb.ToUpper());
            }
        }

        public async Task<APIResponse> EjecutarEstrategia(T objecto, ParametersRequest parametersRequest = null)
        {
            parametersRequest = parametersRequest ?? new ParametersRequest();
            await EstrategiaEnvio.ContruirURL(parametersRequest);
            var response = await EstrategiaEnvio.SendRequest(objecto);
            return response;
        }
        #endregion Métodos
    }
}
