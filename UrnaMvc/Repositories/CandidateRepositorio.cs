using Microsoft.EntityFrameworkCore;
using UrnaMvc.Data;
using UrnaMvc.Interfaces;
using UrnaMvc.Models;

namespace UrnaMvc.Repositories
{
    public class CandidateRepositorio : RepositorioGenerico<Candidate>, ICandidateRepositorio
    {
        private readonly DataContext _context;

        public CandidateRepositorio(DataContext context) : base(context)
        {
            _context = context;
        }

        public Candidate PesquisarPorLegenda(int legenda)
        {
            try
            {
                return _context.Candidate.Where(x => x.Legenda == legenda).FirstOrDefault();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<Candidate> PesquisarPorNome(string descricao)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(descricao))
                    return _context.Candidate.OrderBy(x => x.NomeCompleto).ToList<Candidate>();
                else
                    return _context.Candidate.Where(x => x.NomeCompleto.Contains(descricao)).ToList();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public new async Task<List<Candidate>> ListarTodos()
        {
            try
            {
                return await _context.Candidate.Include(x => x.ListaVotos).ToListAsync();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
