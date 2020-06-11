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

        void Button_Clicked(object sender, EventArgs e)
        {
            var dbPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments),
                "UserDB.db");
            var db = new SQLiteConnection(dbPath);
            //db.CreateTable<RegisterUserTable>();

            //var item = new RegisterUserTable()
            //{
            //    Username = EntryUser.Text,
            //    Password = EntryPass.Text
            //};
            //db.Insert(item);
            var myQuery = db.Table<RegisterUserTable>().Where(u => u.Username.Equals(EntryUser.Text)
            && u.Password.Equals(EntryPass.Text)).FirstOrDefault();

            if(myQuery!=null)
            {
                App.Current.MainPage = new NavigationPage(new Menu());
            }

            else
            {
                Device.BeginInvokeOnMainThread(async () =>
                {
                    var result = await this.DisplayAlert("Error", "Usuario o Password incorrectos!", "Yes", "Cancel");

                    if (result)
                    {
                        await Navigation.PushAsync(new MainPage());
                    }
                    else
                    {
                        await Navigation.PushAsync(new MainPage());
                    }
                });
            }
        }

        async void Button_Clicked_1(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new RegistroView());

        }
    }
}
