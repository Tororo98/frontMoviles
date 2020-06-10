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
        [JsonProperty("idPlato")]
        public long IdPlato { get; set; }

        [JsonProperty("valor")]
        private double valor;

        [JsonProperty("nombre")]

        private string nombre;

        [JsonProperty("descripcion")]
        private string descripcionPlato;
        #endregion properties

        #region Initialize
        public PlatoModel() { }
        #endregion Initialize

        #region Gets & Sets
        public double Valor
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

        public class YourUsersData
        {
            [JsonProperty("plates")]
            public List<PlatoModel> Plates { get; set; }
        }
        #endregion Gets & Sets
    }
}
