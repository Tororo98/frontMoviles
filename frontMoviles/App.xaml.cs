using frontMoviles.Services.Navigation;
using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using frontMoviles.Views;

namespace frontMoviles
{
    public partial class App : Application
    {
        #region Properties
        static NavigationService navigationService;
        #endregion

        #region Getters & Setters
        public static NavigationService NavigationService
        {
            get
            {
                if (navigationService == null)
                {
                    navigationService = new NavigationService();
                }
                return navigationService;
            }
        }
        #endregion Getters & Setters
        public App()
        {
            InitializeComponent();

            MainPage = new NavigationPage(new NewRegistroView());
        }

        protected override void OnStart()
        {
            //CrossFirebasePushNotification.Current.Subscribe("general");
            //CrossFirebasePushNotification.Current.OnTokenRefresh += (s, p) =>
            //{
            //    System.Diagnostics.Debug.WriteLine($"TOKEN : {p.Token}");
            //};
        }

        protected override void OnSleep()
        {
            // Handle when your app sleeps
        }

        protected override void OnResume()
        {
            // Handle when your app resumes
        }
    }
}
