using frontMoviles.Table;
using frontMoviles.ViewModels;
using frontMoviles.Views;
using SQLite;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace frontMoviles
{
    // Learn more about making custom code visible in the Xamarin.Forms previewer
    // by visiting https://aka.ms/xamarinforms-previewer
    [DesignTimeVisible(false)]
    public partial class MainPage : ContentPage
    {
        LoginViewModel context = new LoginViewModel();
        public MainPage()
        {
            InitializeComponent();
            BindingContext = context;
        }
    }
}
