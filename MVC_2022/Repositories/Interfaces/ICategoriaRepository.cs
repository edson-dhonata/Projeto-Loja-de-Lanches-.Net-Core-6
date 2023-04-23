using MVC_2022.Models;

namespace MVC_2022.Repositories.Interfaces
{
    public interface ICategoriaRepository
    {
        IEnumerable<Categoria> Categorias { get; }  
    }
}
 