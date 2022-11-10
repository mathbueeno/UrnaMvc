using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using UrnaMvc.Models;

namespace UrnaMvc.Mapping
{
    public class VotingMap : IEntityTypeConfiguration<Voting>
    {
        public void Configure(EntityTypeBuilder<Voting> builder)
        {
            builder.ToTable("voting");
            builder.HasKey(v => v.Id);

            builder.Property(v => v.Id).HasColumnName("id");
            builder.Property(v => v.CandidateId).HasColumnName("candidate_id");
            builder.Property(v => v.DataVoto).HasColumnName("data_voto");


            builder.Property(v => v.CandidateId).IsRequired(false);
            builder.Property(v => v.DataVoto).IsRequired();

            builder.HasOne(v => v.Candidate).WithMany(v => v.ListaVotos).HasForeignKey(v => v.CandidateId);
        }
    }
}
