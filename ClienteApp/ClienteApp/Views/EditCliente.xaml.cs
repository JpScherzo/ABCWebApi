
using ClienteApp.ViewModels;
using ClienteApp.Models;
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
	public partial class EditCliente : ContentPage
	{
        private bool _isExcluirVisible;


        public EditCliente (Cliente cliente)
		{
            var editClienteViewModel = new EditClienteViewModel(this);

            editClienteViewModel.SelectedCliente = cliente;

            if (cliente.Nome == null)
            {
                editClienteViewModel.SelectedEstado = editClienteViewModel.Estados.FirstOrDefault(z => z.Sigla == "MG");
                editClienteViewModel.novoCliente = true;
            }
            foreach (Estado UF in editClienteViewModel.Estados)
            {
                if (UF.Sigla == cliente.Estado)
                {
                    editClienteViewModel.SelectedEstado = UF;
                    break;
                }
            }


            BindingContext = editClienteViewModel;


            InitializeComponent();

            if (cliente.Nome == null)
            {
                btnExcluir.IsVisible = false;
            }
            else
            {
                campoCPF.IsEnabled = false;
                campoEmail.IsEnabled = false;
            }

        }


    }
}