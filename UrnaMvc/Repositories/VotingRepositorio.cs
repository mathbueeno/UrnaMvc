using UrnaMvc.Data;
using UrnaMvc.Interfaces;
using UrnaMvc.Models;

namespace UrnaMvc.Repositories
{
    public class VotingRepositorio : RepositorioGenerico<Voting>, IVotingRepositorio
    {
        DataContext _context;

        public VotingRepositorio(DataContext context) : base(context)
        {
            _context = context;
        }

        public int RecuperarQtdVotos(int? candidatoId)
        {
            try
            {
                return _context.Voting.Where(x => x.CandidateId == candidatoId).Count();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
