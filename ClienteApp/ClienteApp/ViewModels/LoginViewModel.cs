using ClienteApp.Services;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace ClienteApp.ViewModels
{
    public class LoginViewModel
    {

        private DataService _dataService = new DataService();

        public Autentica autentica { get; set; }

        public Cliente ClienteLogado = new Cliente();

        public Cliente AutenticaCliente()
        {
            ClienteLogado =  _dataService.AutenticarClientes(autentica);
            return ClienteLogado;
        }

    }
}
