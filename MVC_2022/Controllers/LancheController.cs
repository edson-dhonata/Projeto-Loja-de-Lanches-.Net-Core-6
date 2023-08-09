﻿using Microsoft.AspNetCore.Mvc;
using MVC_2022.Models;
using MVC_2022.Repositories.Interfaces;
using MVC_2022.ViewModels;

namespace MVC_2022.Controllers
{
    public class LancheController : Controller
    {
        //Injeção de dependencia da interface de lanche criada na startap.
        private readonly ILancheRepository _lanche;
        private readonly ICategoriaRepository _categoria;

        public LancheController(ILancheRepository lanche, ICategoriaRepository categoria)
        {
            _lanche = lanche;
            _categoria = categoria;
        }

        //Recebe nome da categoria como parâmetro atraves de rota.
        public IActionResult List(int categoria)
        {
            IEnumerable<Lanche> lanches;
            string categoriaAtual = string.Empty;

            if (categoria == 0)
            {
                lanches = _lanche.Lanches.OrderBy(l => l.LancheId);
                categoriaAtual = "Todos os lanches";
            }
            else
            {
                //Pega lanches com base na categoria:
                lanches = _lanche.Lanches
                                        .Where(l => l.Categoria.CategoriaId == categoria)
                                        .OrderBy(l => l.LancheNome);

                //Pega categoria atual do lanche:
                categoriaAtual = _categoria.Categorias.FirstOrDefault(x => x.CategoriaId == categoria).CategoriaNome;
            }

            //Cria instância da view model e manda para view.
            var lanchesListViewModel = new LancheListViewModel
            {
                Lanches = lanches,
                CategoriaAtual = categoriaAtual
            };

            return View(lanchesListViewModel);
        }

        //Método que exibe detalhes dos lanches
        public IActionResult Details(int lancheId)
        {
            var lanche = _lanche.Lanches.FirstOrDefault(l => l.LancheId == lancheId);
            return View(lanche);
        }

        //Nome do parâmetro aqui tem que estar igual o nome da viewdata no campo search
        public ViewResult Search(string searchString)
        {
            IEnumerable<Lanche> lanches;
            string categoriaAtual = string.Empty;

            if (string.IsNullOrEmpty(searchString))
            {
                lanches = _lanche.Lanches.OrderBy(p => p.LancheId);
                categoriaAtual = "Todos os Lanches";
            }
            else
            {
                lanches = _lanche.Lanches
                          .Where(p => p.LancheNome.ToLower().Contains(searchString.ToLower()));

                //Se contiver lanche retornado.
                if (lanches.Any())
                    categoriaAtual = "Lanches";
                else
                    categoriaAtual = "Nenhum lanche foi encontrado";
            }

            return View("~/Views/Lanche/List.cshtml", new LancheListViewModel
            {
                Lanches = lanches,
                CategoriaAtual = categoriaAtual
            });
        }
    }
}
