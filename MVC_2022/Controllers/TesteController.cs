using Microsoft.AspNetCore.Mvc;
using MVC_2022.Models;
using MVC_2022.ViewModels;

namespace MVC_2022.Controllers
{
    public class TesteController : Controller
    {
        public IActionResult Teste()
        {
            return View();
        }

        public IActionResult PreencherLista() 
        {

            var pessoa1 = new TesteLista { nome = "Edson Santos", idade = 28, nascimento = Convert.ToDateTime("16/10/1994") };
            var pessoa2 = new TesteLista { nome = "Cleice mendes", idade = 27, nascimento = Convert.ToDateTime("10/01/1995") };
            var pessoa3 = new TesteLista { nome = "Emerson Oliveira", idade = 28, nascimento = Convert.ToDateTime("16/10/1994") };

            List<TesteLista> minhaLista = new()
            {
                pessoa1,
                pessoa2,
                pessoa3
            };

            return View(minhaLista);
        }

        [HttpPost]
        public IActionResult Insert([FromBody] List<TesteLista> lista)
        {
            return View("~/Views/Teste/PreencherLista.cshtml", lista);
        } 
    }
}
