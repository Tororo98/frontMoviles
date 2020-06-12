using frontMoviles.Models;
using frontMoviles.Models.AuxiliarModels;
using Newtonsoft.Json;
using Rg.Plugins.Popup.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace frontMoviles.ViewModels
{
    class InfoViewModel : ViewModelBase
    {
        private ObservableCollection<PlatoModel> platos;
        public ObservableCollection<PlatoModel> Platos
        {
            get { return platos; }
            set
            {
                platos = value;
                OnPropertyChanged();
            }
        }
        public InfoViewModel(ObservableCollection<PlatoModel> listaPlatos)
        {
            platos = listaPlatos;
        }

        
    }
}
