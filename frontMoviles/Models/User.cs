using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace frontMoviles.Models
{
    public class User : BaseModel
    {
        #region properties
        [JsonProperty("idUsuario")]
        public long UserId { get; set; }

        [JsonProperty("nombres")]
        private string nombre;

        [JsonProperty("apellidos")]
        private string apellido;

        [JsonProperty("contrasena")]
        public string Password { get; set; }

        [JsonProperty("correo")]
        public string Correo { get; set; }

        [JsonProperty("idRol")]
        public int IdRol { get; set; }
        #endregion properties

        #region Initialize
        public User() { }
        #endregion Initialize

        #region Getters & Setters
        public string Nombre
        {
            get { return nombre; }
            set
            {
                nombre = value;
                OnPropertyChanged();
            }
        }

        public string Apellido
        {
            get { return apellido; }
            set
            {
                apellido = value;
                OnPropertyChanged();
            }
        }

        #endregion Getters & Setters
    }
}
