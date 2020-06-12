using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using frontMoviles.Models;
using frontMoviles.Services.Navigation;
using frontMoviles.ViewModels;
using frontMoviles.Views;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace frontMoviles
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Menu : ContentPage
    {
        public Menu()
        {
            InitializeComponent();
            BindingContext = new MenuViewModel();
        }

        //private async void OnItemSelected(Object sender, ItemTappedEventArgs e)
        //{
        //    var details = e.Item as MenuModel;
        //    await Navigation.PushAsync(new MenuDetail(details.Nombre));
        //}
        //async void Button_Clicked(object sender, EventArgs e)
        //{
        //    await Navigation.PushAsync(new MainPage());
        //}

        private async void ListView_ItemTapped(object sender, ItemTappedEventArgs e)
        {
            var details = e.Item as MenuModel;
            await Navigation.PushAsync(new MenuDetail(details.Nombre, details.Descripcion, details.Valor));
        }
    }
}