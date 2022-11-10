using Microsoft.EntityFrameworkCore;
using UrnaMvc.Mapping;
using UrnaMvc.Models;

namespace UrnaMvc.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {
        }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.ApplyConfiguration(new CandidateMap());
            builder.ApplyConfiguration(new VotingMap());
        }

        public DbSet<Candidate> Candidate { get; set; }
        public DbSet<Voting> Voting { get; set; }
    }
}
