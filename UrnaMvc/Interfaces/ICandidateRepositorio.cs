using UrnaMvc.Models;

namespace UrnaMvc.Interfaces
{
    // Padrão de Design Pattern - Interface
     
    public interface ICandidateRepositorio : IRepositorioGenerico<Candidate>
    {
        List<Candidate> PesquisarPorNome(string descricao);
        Candidate PesquisarPorLegenda(int legenda);
        new Task<List<Candidate>> ListarTodos();
    }
}
