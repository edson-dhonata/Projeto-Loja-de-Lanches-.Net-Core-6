using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using MVC_2022.ViewModels;

namespace MVC_2022.Controllers
{
    public class AccountController : Controller
    {
        //Representa um usuário no sistema de autenticação.
        private readonly UserManager<IdentityUser> _userManager;

        //Essa classe é responsável por gerenciar a funcionalidade de login e logout dos usuários no sistema.
        private readonly SignInManager<IdentityUser> _signInManager;

        public AccountController(UserManager<IdentityUser> userManager,SignInManager<IdentityUser> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }

        //Método para apresentar o formulário de autenticação:
        public IActionResult Login(string returnUrl)
        {
            return View(new LoginViewModel()
            {
                ReturnUrl = returnUrl
            });
        }

        // Método que irá fazer a autenticação do usuário após o post do formulário.
        // Todas as propriedades de _userManager e _signInmanager são acincronas, por isso o método tem que ser asicrono.
        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel loginVM)
        {
            if (!ModelState.IsValid)
                return View(loginVM);

            var user = await _userManager.FindByNameAsync(loginVM.UserName);

            if (user != null)
            {
                // O primeiro parâmetro de false e a respeito do cookie de login ao fechar e abrir o navegador,
                // O segundo e para bloqueio de conta ou não ao errar o login.
                var result = await _signInManager.PasswordSignInAsync(user, loginVM.Password, false, false);
                if (result.Succeeded)
                {
                    if (string.IsNullOrEmpty(loginVM.ReturnUrl))
                    {
                        return RedirectToAction("Index", "Home");
                    }
                    return Redirect(loginVM.ReturnUrl);
                }
            }
            ModelState.AddModelError("", "Falha ao realizar o login!!");
            return View(loginVM);
        }
    }
}
