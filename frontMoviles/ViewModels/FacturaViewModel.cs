using frontMoviles.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace frontMoviles.ViewModels
{
    public class FacturaViewModel : ViewModelBase
    {
        #region Properties

        private List<PlatoModel> lista_plates;

        #endregion Properties

        #region initialize
        public FacturaViewModel(List<PlatoModel> listPlates)
        {
            lista_plates = listPlates;
            Console.WriteLine(lista_plates[0].Nombre);
            Console.WriteLine(lista_plates[0].DescripcionPlato);

        }
        #endregion initialize

    }
}
