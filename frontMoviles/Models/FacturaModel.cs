using System;
using System.Collections.Generic;
using System.Text;

namespace frontMoviles.Models
{
    public class FacturaModel : BaseModel
    {
        #region properties
        public int IdFactura { get; set; }

        //private User empleado;

        private string cliente;

        private string cedula { get; set; }

        private DateTime fechaFactura;

        private float descuento;

        private float valorTotal;
        #endregion properties

        #region Initialize 
        public FacturaModel(/*User empleado*/)
        {
            //this.empleado = empleado;
        }
        #endregion Initialize

        #region gets & sets

        public string Cliente
        {
            get { return cliente; }
            set { cliente = value; OnPropertyChanged(); }
        }

        public string Cedula
        {
            get { return cedula; }
            set { cedula = value; OnPropertyChanged(); }
        }

        public DateTime FechaFactura
        {
            get { return fechaFactura; }
            set { fechaFactura = value; OnPropertyChanged(); }
        }

        public float ValorTotal
        {
            get { return valorTotal; }
            set { valorTotal = value; OnPropertyChanged(); }
        }
        public float Descuento
        {
            get { return descuento; }
            set { descuento = value; OnPropertyChanged();}
        }
        /*
        public User Empleado
        {
            get { return empleado; }
            set { empleado = value; }
        }
        */
        #endregion gets & sets

    }
}
