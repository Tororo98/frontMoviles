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
            FoodList.Add(new MenuModel { Nombre = "Pollito", Valor="8500", Descripcion = "Pollo curry"});
            FoodList.Add(new MenuModel { Nombre = "Bandeja Paisa", Valor = "10000", Descripcion = "Frijoles, chicharron, arroz" });
            FoodList.Add(new MenuModel { Nombre = "Limonada", Valor = "4500", Descripcion = "Refrescante..." });
            FoodList.Add(new MenuModel { Nombre = "Chuleta", Valor = "9000", Descripcion = "Cerdo" });

        }

        #endregion Initialize
    }
}
