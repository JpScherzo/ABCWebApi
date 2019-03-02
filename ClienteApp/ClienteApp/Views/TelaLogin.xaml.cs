using ClienteApp.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace ClienteApp.Views
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class TelaLogin : ContentPage
	{
        public LoginViewModel loginViewModel;
        public int IniciandoApp;

        public TelaLogin (int flag)
		{
            IniciandoApp = flag;

            loginViewModel = new LoginViewModel();

            loginViewModel.autentica = new Autentica();

            BindingContext = loginViewModel;

            InitializeComponent();
        }
        

        private void Button_Clicked_Entrar(object sender, EventArgs e)
        {
            App.ClienteAtual = loginViewModel.AutenticaCliente();
           

            if (loginViewModel.ClienteLogado is null)
            {
                messageLogin.Text = "Seu e-mail e/ou senha estão incorretos!";
                SenhaEntry.Text = string.Empty;
            }
            else
            {
                App.IsUserLoggedIn = true;
                if (IniciandoApp == 1)
                {
                    Navigation.InsertPageBefore(new MainPage(), this);
                    Application.Current.MainPage.Navigation.PopAsync();
                }
                else
                {
                    Application.Current.MainPage.Navigation.PopAsync();
                }

            }
        }

        private void Button_Clicked_Novo(object sender, EventArgs e)
        {
            var cliente = new Cliente();
            Navigation.PushAsync(new EditCliente(cliente));
        }
    }
}