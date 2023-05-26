using MessagePack.Formatters;
using Microsoft.AspNetCore.Mvc;
using MVC_2022.Models;
using MVC_2022.Repositories;
using MVC_2022.Repositories.Interfaces;
using MVC_2022.ViewModels;

namespace MVC_2022.Controllers
{
    public class LancheController : Controller
    {
        //Injeção de dependencia da interface de lanche criada na startap.
        private readonly ILancheRepository _lanche;

        public LancheController(ILancheRepository lanche)
        {
            _lanche = lanche;
        }

        //Recebe nome da categoria como parâmetro atraves de rota.
        public IActionResult List(string categoria)
        {
            IEnumerable<Lanche> lanches;
            string categoriaAtual = string.Empty;

            if (string.IsNullOrEmpty(categoria))
            {
                lanches = _lanche.Lanches.OrderBy(l => l.LancheId);
                categoriaAtual = "Todos os lanches";
            }
            else
            {
                //Compara se o nome da categoria e igual a Normal e ignora CaseSensitive
                if (string.Equals("Normal", categoria, StringComparison.OrdinalIgnoreCase))
                {
                    lanches = _lanche.Lanches
                        .Where(l => l.Categoria.CategoriaNome.Equals("Normal"))
                        .OrderBy(l => l.LancheNome);
                }
                else
                {
                    lanches = _lanche.Lanches
                       .Where(l => l.Categoria.CategoriaNome.Equals("Natural"))
                       .OrderBy(l => l.LancheNome);
                }
                categoriaAtual = categoria;
            }

            //Cria instância da view model e manda para view.
            var lanchesListViewModel = new LancheListViewModel
            {
                Lanches = lanches,
                CategoriaAtual = categoriaAtual
            };

            return View(lanchesListViewModel);
        }
    }
}
