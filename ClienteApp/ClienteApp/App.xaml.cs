using ClienteApp.Views;
using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using ClienteApp.ViewModels;


[assembly: XamlCompilation(XamlCompilationOptions.Compile)]
namespace ClienteApp
{
    public partial class App : Application

    {
        public static bool IsUserLoggedIn { get; set; }
        public static Cliente ClienteAtual { get; set; }

        public App()
        {
            if (!IsUserLoggedIn)
            {
                MainPage = new NavigationPage(new TelaLogin(1));
            } else
            {
                MainPage = new NavigationPage(new MainPage());
            }
        }

        protected override void OnStart()
        {
            // Handle when your app starts
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
