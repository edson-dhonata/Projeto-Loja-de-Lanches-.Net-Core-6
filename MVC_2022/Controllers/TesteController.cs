using Microsoft.AspNetCore.Mvc;
using MVC_2022.ViewModels;

namespace MVC_2022.Controllers
{
    public class TesteController : Controller
    {
        class Testelista
        {
            public int quantidade { get; set; }
            public DateTime data { get; set; }
        }



        public IActionResult Teste()
        {


            List<Testelista> Lista1 = new List<Testelista>();
            List<Testelista> Lista2 = new List<Testelista>();

            var conteudo1 = new Testelista { quantidade= 2, data = DateTime.Now };
            var conteudo2 = new Testelista { quantidade= 1, data = DateTime.Now.AddDays(1) };
            
            Lista1.Add(conteudo1);
            Lista1.Add(conteudo2);            
            
            var conteudo3 = new Testelista { quantidade= 2, data = DateTime.Now };
            var conteudo4 = new Testelista { quantidade= 1, data = DateTime.Now.AddDays(1) };
            
            Lista2.Add(conteudo3);
            Lista2.Add(conteudo4);

            var testelista1 = Lista1.AsQueryable().Select(x => new { DATA = x.data.Date, QTD = x.quantidade });
            var testelista2 = Lista2.AsQueryable().Select(x => new { DATA = x.data.Date, QTD = x.quantidade });

            var result = testelista1.Join(testelista2,
                              x => x.DATA,
                              y => y.DATA,
                              (x, y) => new { DATA = x.DATA, QTD1 = (x.QTD + y.QTD) });






            return View();
        }

        public IActionResult Insert(TesteEdson meuModelo) 
        {





            return View("~/Views/Teste/Teste.cshtml",meuModelo);
        }
    }
}
