using System;
using System.Collections.Generic;
using System.Text;

namespace frontMoviles.Models
{
    public class FacturaModel : NotificationObject
    {
        #region properties
        public int IdFactura { get; set; }

        private User empleado;

        public string Cliente { get; set; }

        public string IdCliente { get; set; }

        public DateTime FechaFactura { get; set; }

        public Double Descuento { get; set; }

        private Double valorTotal;
        #endregion properties

        #region Initialize 
        public FacturaModel(User empleado)
        {
            this.empleado = empleado;
        }
        #endregion Initialize

        #region gets & sets
        public Double ValorTotal
        {
            get { return valorTotal; }
            set { valorTotal = value; OnPropertyChanged();}
        }

        public User Empleado
        {
            get { return empleado; }
            set { empleado = value; }
        }
        #endregion gets & sets



    }
}
