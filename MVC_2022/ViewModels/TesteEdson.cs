using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.Contracts;

namespace MVC_2022.ViewModels
{
    public class TesteEdson
    {
        public TesteEdson() 
        { 
            this.telefones = new List<Telefone>();   
            this.enderecos = new List<Endereco>();   
        }

        [ScaffoldColumn(false)]
        public int codigo { get; set; }
        [DisplayName("Nome:")]
        public string nome { get; set; }
        [DisplayName("Data de Nascimento:")]
        public DateTime dataNascimento { get; set; }
        public List<Telefone> telefones { get; set; }
        public List<Endereco> enderecos { get; set; }

    }

    public class Telefone
    {
        public int tipo { get; set; }
        public string numero { get; set; }
        public string operadora { get; set; }
    }

    public class Endereco
    {
        public string cep { get; set; }
        public string logradouro { get; set; }
        public string bairro { get; set; }
        public string cidade  { get; set; }
        public string estado { get; set; }
    }
}
