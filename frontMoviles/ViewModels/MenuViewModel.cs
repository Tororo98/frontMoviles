using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Windows.Input;
using frontMoviles.Models;
using Xamarin.Forms;

namespace frontMoviles.ViewModels
{
    public class MenuViewModel
    {
        #region attributes
        public ObservableCollection<MenuModel> FoodList { get; set; }

        #endregion attributes

        #region Commands

        #endregion Commands

        #region Metodos

        #endregion Metodos

        #region Initialize
        public MenuViewModel()
        {
            FoodList = new ObservableCollection<MenuModel>();
            FoodList.Add(new MenuModel { Nombre = "Pollito", Valor = 8500, Descripcion = "Pollo curry" });
        }

        #endregion Initialize
    }
}