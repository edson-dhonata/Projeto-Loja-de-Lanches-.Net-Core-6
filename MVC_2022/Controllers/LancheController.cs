using Microsoft.AspNetCore.Mvc;
using MVC_2022.Repositories.Interfaces;

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
            //Devolvendo lista de lanche para view.
            return View(_lanche.Lanches); ;
        }
    }
}
