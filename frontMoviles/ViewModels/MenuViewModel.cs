using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;
using frontMoviles.Models;
using Xamarin.Forms;

namespace frontMoviles.ViewModels
{
    public class MenuViewModel
    {
        #region Commands
        public ICommand SeleccionarPlatoCommand { get; set; }
        #endregion Commands
        //public MenuViewModel()
        //{
        //    SeleccionarPlatoCommand = new Command(() => SeleccionarPlato(), () => true);
        //}

        #region Metodos
        //public void SeleccionarPlato()
        //{
        //    var x = test;
        //}
        #endregion Metodos
    }
}
