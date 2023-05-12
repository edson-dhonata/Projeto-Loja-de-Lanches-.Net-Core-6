using Microsoft.AspNetCore.Mvc;
using MVC_2022.ViewModels;

namespace MVC_2022.Controllers
{
    public class TesteController : Controller
    {
        public IActionResult Teste()
        {
            return View();
        }

        public IActionResult Insert(TesteEdson meuModelo) 
        {





            return View("~/Views/Teste/Teste.cshtml",meuModelo);
        }
    }
}
