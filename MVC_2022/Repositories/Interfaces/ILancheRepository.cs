using MVC_2022.Models;

namespace MVC_2022.Repositories.Interfaces
{
    //Interface de lanche:
    public interface ILancheRepository
    {
        IEnumerable<Lanche> Lanches { get; }
        IEnumerable<Lanche> LanchesPreferidos { get; }
        Lanche GetLancheById(int lancheId);
    }
}
