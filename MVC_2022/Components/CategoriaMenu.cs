using Microsoft.AspNetCore.Mvc;
using MVC_2022.Models;
using MVC_2022.Repositories.Interfaces;

namespace MVC_2022.Components
{
    //Classe herdando de View Component
    public class CategoriaMenu : ViewComponent
    {
        private readonly ICategoriaRepository _repository;

        public CategoriaMenu(ICategoriaRepository repository)
        {
            _repository = repository;
        }

        public IViewComponentResult Invoke()
        {
            var categorias = _repository.Categorias.OrderBy(x => x.CategoriaNome);

            return View(categorias);
        }
    }
}
