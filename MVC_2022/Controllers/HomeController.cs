using Microsoft.AspNetCore.Mvc;
using MVC_2022.Models;
using MVC_2022.Repositories.Interfaces;
using MVC_2022.ViewModels;
using System.Diagnostics;
using System.Runtime.CompilerServices;

namespace MVC_2022.Controllers
{
    //Controller principal
    public class HomeController : Controller
    {
        private readonly ILancheRepository _lancheRepository;

        public HomeController(ILancheRepository lancheRepository)
        {
            _lancheRepository = lancheRepository;
        }

        public IActionResult Index()
        {
            var homeViewModel = new HomeViewModel
            {
                LanchesPreferidos = _lancheRepository.LanchesPreferidos
            };

            //TempData["obsTemp"] = "Observação TemData HomeController.";
            return View(homeViewModel);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}