using UrnaMvc.Models;

namespace UrnaMvc.Interfaces
{
    public interface ICandidateRepositorio : IRepositorioGenerico<Candidate>
    {
        List<Candidate> PesquisarPorNome(string descricao);
        Candidate PesquisarPorLegenda(int legenda);
        new Task<List<Candidate>> ListarTodos();
    }
}
