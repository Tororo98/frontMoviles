using System;
using System.Collections.Generic;
using System.Text;

namespace frontMoviles.Models
{
    public class User
    {
        #region properties
        public long UserId { get; set; }
        public string Nombre { get; set; }
        public string Apellido { get; set; }
        public string Username { get; set; }        
        public string Password { get; set; }
        public string Correo { get; set; }
        #endregion properties

        #region Initialize
        public User() { }
        #endregion Initialize
    }
}
