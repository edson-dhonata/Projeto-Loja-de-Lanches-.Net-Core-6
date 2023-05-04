using Microsoft.AspNetCore.Mvc;
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
        public IActionResult List()
        {
            //Exemplo de ViewBag, ViewData
            ViewBag.Observacao = "Observação vinda da ViewBag";
            ViewData["obsData"] = "Observação da ViewData as " + DateTime.Now;

            //Instancia classe ViewModel
            LancheListViewModel info = new LancheListViewModel();
            info.CategoriaAtual = "Lanche categoria atual";
            info.Lanches = _lanche.Lanches;

            //Devolvendo lista de lanche para view.
            return View(info);
        }
    }
}
