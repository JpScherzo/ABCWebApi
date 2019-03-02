using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace ClienteApp.Services
{
    public class DataService
    {
        public string Url = "https://cliente-app.azurewebsites.net/api/clientes/";

        public async Task<List<Cliente>> GetClientes()
        {
            var httpClient = new HttpClient();

            var json = await httpClient.GetStringAsync(Url);

            var clientes = JsonConvert.DeserializeObject<List<Cliente>>(json);

            return (clientes);
        }

        public async Task PostCliente(Cliente cliente)
        {
            var httpClient = new HttpClient();

            var json = JsonConvert.SerializeObject(cliente);

            StringContent content = new StringContent(json);
            content.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue("application/json");

            var result = await httpClient.PostAsync(Url, content);
        }

        public async Task PutCliente(long CPF, Cliente cliente)
        {
            var httpClient = new HttpClient();

            var json = JsonConvert.SerializeObject(cliente);

            StringContent content = new StringContent(json);
            content.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue("application/json");

            var result = await httpClient.PutAsync(Url + CPF, content);
        }

        public async Task DeleteClientes(long CPF)
        {
            var httpClient = new HttpClient();

            var response = await httpClient.DeleteAsync(Url + CPF);

        }

        public Cliente AutenticarClientes(Autentica Auth)
        {
            var httpClient = new HttpClient();

            var json = JsonConvert.SerializeObject(Auth);

            StringContent content = new StringContent(json);
            content.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue("application/json");

            var response = httpClient.PostAsync(Url + "autenticar", content).Result;
            Cliente clientelogado = null;
            
            if (response.IsSuccessStatusCode)
            {
                var responseContent = response.Content;
                var responseString = response.Content.ReadAsStringAsync().Result;

                clientelogado = JsonConvert.DeserializeObject<Cliente>(responseString);
            }

            return (clientelogado);

        }

        public bool EmailEmUso(string email)
        {
            var httpClient = new HttpClient();

            Autentica Auth = new Autentica();

            Auth.Email = email;

            var json = JsonConvert.SerializeObject(Auth);

            StringContent content = new StringContent(json);
            content.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue("application/json");

            var response = httpClient.PostAsync(Url + "verificar", content).Result;
            Cliente clientelogado = null;

            if (response.IsSuccessStatusCode)
            {
                if (response.StatusCode == System.Net.HttpStatusCode.NoContent)
                    return (true);
            }
            return (false);
        }

    }
}
