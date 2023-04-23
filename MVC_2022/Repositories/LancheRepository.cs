using Microsoft.EntityFrameworkCore;
using MVC_2022.Context;
using MVC_2022.Models;
using MVC_2022.Repositories.Interfaces;

namespace MVC_2022.Repositories
{
    public class LancheRepository:ILancheRepository
    {
        private readonly AppDbContext _context;

        public LancheRepository(AppDbContext contexto)
        {
            _context = contexto;
        }

        // O método Include do link, permite obter os dados relacionados incluindo-os no resultado da consulta.
        // Aqui estou retornando uma lista de lanches, e suas categorias.
        public IEnumerable<Lanche> Lanches => _context.Lanches.Include(c => c.Categoria);

        public IEnumerable<Lanche> LanchesPreferidos => _context.Lanches.Where(l => l.LancheIsPreferido).Include(c => c.Categoria);
        
        public Lanche GetLancheById(int lancheId)
        {
            return _context.Lanches.FirstOrDefault(l => l.LancheId == lancheId);
        }
    }
}
