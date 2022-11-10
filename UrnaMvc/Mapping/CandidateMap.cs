using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using UrnaMvc.Models;

namespace UrnaMvc.Mapping
{
    public class CandidateMap : IEntityTypeConfiguration<Candidate>
    {
        public void Configure(EntityTypeBuilder<Candidate> builder)
        {
            builder.ToTable("candidate");
            builder.HasKey(c => c.Id);

            builder.Property(c => c.Id).HasColumnName("id");
            builder.Property(c => c.NomeCompleto).HasColumnName("nome_completo");
            builder.Property(c => c.NomeVice).HasColumnName("nome_vice");
            builder.Property(c => c.DataRegistro).HasColumnName("data_registro");
            builder.Property(c => c.Legenda).HasColumnName("legenda");

            builder.Property(c => c.NomeCompleto).HasMaxLength(50);
            builder.Property(c => c.NomeVice).HasMaxLength(50);

            builder.Property(c => c.NomeCompleto).IsRequired();
            builder.Property(c => c.NomeVice).IsRequired();
            builder.Property(c => c.Legenda).IsRequired();
        }
    }
}
