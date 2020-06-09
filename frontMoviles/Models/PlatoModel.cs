using System;
using System.Collections.Generic;
using System.Text;
using frontMoviles.Models;

namespace frontMoviles.Models
{
    class PlatoModel : NotificationObject
    {
        #region properties
        public long IdPlato { get; set; }
        private double valor;
        private string nombre;
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
        #endregion Gets & Sets
    }
}
