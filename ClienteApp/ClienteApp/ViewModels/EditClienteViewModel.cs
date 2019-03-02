using ClienteApp.Services;
using ClienteApp.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;


namespace ClienteApp.ViewModels
{
    public class EditClienteViewModel : INotifyPropertyChanged
    {
        private DataService _dataService = new DataService();

        public Cliente SelectedCliente { get; set; }

        public bool novoCliente = false; 

        public List<Estado> Estados { get; set; }

        public Estado _elementoSelecionado;

        public Estado SelectedEstado
        {
            get => _elementoSelecionado;
            set {
                _elementoSelecionado = value; PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(SelectedEstado)));
                SelectedCliente.Estado = _elementoSelecionado.Sigla;
            }
        }

        public Page viewPage;

        public EditClienteViewModel(Page page)
        {
            viewPage = page;

            Estados = new List<Estado>
            {
                new Estado {Sigla="AC", Indice = 0},
                new Estado {Sigla="AL", Indice = 1},
                new Estado {Sigla="AM", Indice = 2},
                new Estado {Sigla="AP", Indice = 3},
                new Estado {Sigla="BA", Indice = 4},
                new Estado {Sigla="CE", Indice = 5},
                new Estado {Sigla="DF", Indice = 6},
                new Estado {Sigla="ES", Indice = 7},
                new Estado {Sigla="GO", Indice = 8},
                new Estado {Sigla="MA", Indice = 9},
                new Estado {Sigla="MG", Indice = 10},
                new Estado {Sigla="MS", Indice = 11},
                new Estado {Sigla="MT", Indice = 12},
                new Estado {Sigla="PA", Indice = 13},
                new Estado {Sigla="PB", Indice = 14},
                new Estado {Sigla="PE", Indice = 15},
                new Estado {Sigla="PI", Indice = 16},
                new Estado {Sigla="PR", Indice = 17},
                new Estado {Sigla="RJ", Indice = 18},
                new Estado {Sigla="RN", Indice = 19},
                new Estado {Sigla="RO", Indice = 20},
                new Estado {Sigla="RR", Indice = 21},
                new Estado {Sigla="RS", Indice = 22},
                new Estado {Sigla="SC", Indice = 23},
                new Estado {Sigla="SE", Indice = 24},
                new Estado {Sigla="SP", Indice = 25},
                new Estado {Sigla="TO", Indice = 26},
            };

        }

        public bool ValidaCliente()
        {
            if ((SelectedCliente.CPF == 0) ||
                (string.IsNullOrEmpty(SelectedCliente.Email)) ||
                (string.IsNullOrEmpty(SelectedCliente.Endereco)) ||
                (string.IsNullOrEmpty(SelectedCliente.Estado)) ||
                (string.IsNullOrEmpty(SelectedCliente.Municipio)) ||
                (string.IsNullOrEmpty(SelectedCliente.Nome)) ||
                (string.IsNullOrEmpty(SelectedCliente.Senha)) ||
                (string.IsNullOrEmpty(SelectedCliente.Telefone)))
            {
                return (false);
            }
            else
            {
                return (true);
            }

        }
        public ICommand GravarEditCommand => new Command(async () =>
        {
            SelectedCliente.Perfil = 0;
            if (!ValidaCliente())
            {
                var result = await viewPage.DisplayAlert("Atenção", "Dados informados inválidos!", "OK","Cancela");
            } else
            {
                if (!novoCliente)
                {
                    await _dataService.PutCliente(SelectedCliente.CPF, SelectedCliente);
                    await viewPage.DisplayAlert("Informação", "Cliente alterado com sucesso!", "OK", " ");
                }
                else
                {
                    SelectedCliente.Perfil = 0;
                    if (_dataService.EmailEmUso(SelectedCliente.Email))
                    {
                        var result = await viewPage.DisplayAlert("Atenção", "E-mail já cadastrado!", "OK", "Cancela");
                    } else
                    {
                        await _dataService.PostCliente(SelectedCliente);
                        await viewPage.DisplayAlert("Informação", "Cliente incluído com sucesso!", "OK", " ");
                        await viewPage.Navigation.PopAsync();
                    }
                }
            }
        });

        public ICommand ExcluirEditCommand => new Command(async () =>
        {
            var result = await viewPage.DisplayAlert("Atenção", "Confirma a exclusão deste usuário?", "OK", "Cancela");
            if (result)
            {
                await _dataService.DeleteClientes(SelectedCliente.CPF);
                await viewPage.DisplayAlert("Informação", "Cliente excluído com sucesso!", "OK", " ");
                App.IsUserLoggedIn = false;
                await viewPage.Navigation.PopAsync();
            }
        });

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

    }
}
