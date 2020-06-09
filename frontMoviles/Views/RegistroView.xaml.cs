using frontMoviles.Table;
using SQLite;
using System;
using frontMoviles.ViewModels;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace frontMoviles.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class RegistroView : ContentPage
    {
        RegistroViewModel context = new RegistroViewModel();
        public RegistroView()
        {
            InitializeComponent();
            BindingContext = context;
        }

    //    private void Button_Clicked(object sender, EventArgs e)
    //    {
    //        var dbPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments),
    //    "UserDB.db");
    //        var db = new SQLiteConnection(dbPath);
    //        db.CreateTable<RegisterUserTable>();

    //        var item = new RegisterUserTable()
    //        {
    //            Username = EntryUser.Text,
    //            Password = EntryPass.Text
    //        };
    //        db.Insert(item);
    //        Device.BeginInvokeOnMainThread(async () =>
    //        {
    //            var result = await this.DisplayAlert("Felicidades", "ya estas registrado!", "Yes", "Cancel");

    //            if (result)
    //            {
    //                await Navigation.PushAsync(new MainPage());
    //            }
    //        });
    //    }
    }
}