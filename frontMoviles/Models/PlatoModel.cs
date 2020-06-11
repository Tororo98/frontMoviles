using System;
using System.Collections.Generic;
using System.Text;
using frontMoviles.Models;
using Newtonsoft.Json;

namespace frontMoviles.Models
{

    public class PlatoModel : BaseModel
    {
        #region properties
        [JsonProperty("id")]
        public int IdPlato { get; set; }

        [JsonProperty("nombre")]

        private string nombre;

        [JsonProperty("valor")]
        private float valor;

        [JsonProperty("descripcion")]
        private string descripcionPlato;
        #endregion properties

        #region Initialize
        public PlatoModel() { }
        #endregion Initialize

        #region Gets & Sets
        public float Valor
        {
            get { return valor; }
            set { valor = value; OnPropertyChanged(); }
        }
        public string Nombre
        {
            get { return nombre; }
            set { nombre = value; OnPropertyChanged(); }
        }

        public string DescripcionPlato
        {
            get { return descripcionPlato; }
            set { descripcionPlato = value; OnPropertyChanged(); }
        }
        #endregion Gets & Sets
    }


}
