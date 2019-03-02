using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ABCWebApi.Models
{
    public class Cliente
    {
        [System.ComponentModel.DataAnnotations.Key]
        public Int64 CPF{ get; set; }

        public string Nome { get; set; }
        public string Endereco { get; set; }
        public string Estado { get; set; }
        public string Municipio { get; set; }
        public string Telefone { get; set; }
        public string Email { get; set; }
        public string Senha { get; set; }
        public int Perfil { get; set; }
    }

    public class Autentica
    {
        public string Email { get; set; }
        public string Senha { get; set; }
    }
}
