using MVC_2022.Context;
using MVC_2022.Models;
using MVC_2022.Repositories.Interfaces;

namespace MVC_2022.Repositories
{
    public class CategoriaRepository : ICategoriaRepository
    {
        private readonly AppDbContext _context;

        public CategoriaRepository(AppDbContext context)
        {
            _context = context;
        }

        public IEnumerable<Categoria> Categorias => _context.Categorias;
    }
}
