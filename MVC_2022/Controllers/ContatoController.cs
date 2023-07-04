using Microsoft.AspNetCore.Mvc;

namespace MVC_2022.Controllers
{
    public class ContatoController : Controller
    {
        public IActionResult Index()
        {
            //Permite acesso a página de contato apenas para usuários logados no sistema. Essa opção é a nível de código
            //Pode utilizar também a tag [Authorize] Para permitir acesso apenas aos usuários autenticados em nível de controlador e também dos métodos actions.
            //Tag [AllowAnonymous] pode ser usado para liberar acesso a controladores ou a métodos actions para usuários não atenticados, isso também funciona 
            //em conjunto com o Authorize, aplicando o Authorize no controlador e permitindo acesso aos métodos apenas para usuários autenticados e usando o AllowAnonymous
            //para liberar um método específico no controlador. O contrário não funciona.
            
            //if(User.Identity.IsAuthenticated)
                return View();
           // else
            //    return RedirectToAction("Login","Account");
        }
    }
}
