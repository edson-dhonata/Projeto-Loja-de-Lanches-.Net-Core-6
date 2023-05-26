using Microsoft.AspNetCore.Mvc;

namespace MVC_2022.Controllers
{
    public class ContatoController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
