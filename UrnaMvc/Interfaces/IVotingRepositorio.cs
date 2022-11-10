using UrnaMvc.Models;

namespace UrnaMvc.Interfaces
{
    public interface IVotingRepositorio : IRepositorioGenerico<Voting>
    {
        int RecuperarQtdVotos(int? candidatoId);
    }
}
