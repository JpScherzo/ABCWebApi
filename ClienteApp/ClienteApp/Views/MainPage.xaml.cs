using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using ClienteApp.Views;
using ClienteApp.ViewModels;


namespace ClienteApp
{
    public partial class MainPage : ContentPage
    {
        static readonly List<string> MenuItens = new List<string>()
        {
            "Produtos", "Categorias", "Meu Cadastro", "Meus Pedidos"
        };

        public MainPage()
        {
            if (!App.IsUserLoggedIn)
            {
                Navigation.PushAsync(new TelaLogin(0));
            }
            InitializeComponent();
            lvMenu.ItemsSource = MenuItens;
        }


        private void ListView_ItemTapped(object sender, ItemTappedEventArgs e)
        {
            Cliente cliente = App.ClienteAtual;

            Navigation.PushAsync(new EditCliente(cliente));
        }

        protected override void OnAppearing()
        {
            if (!App.IsUserLoggedIn)
            {
                Navigation.PushAsync(new TelaLogin(0));
            }
        }
    }
}
