using MVC_2022.Context;
using MVC_2022.Models;

namespace MVC_2022.ViewModels
{
    //Classe que vai ser utilizada para alimentar a view List de Lanche.
    public class LancheListViewModel
    {
        public IEnumerable<Lanche> Lanches { get; set; }

        public string CategoriaAtual { get; set; }
    }
}
