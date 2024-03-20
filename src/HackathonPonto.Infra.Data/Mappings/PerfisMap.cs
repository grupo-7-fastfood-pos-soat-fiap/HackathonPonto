
using HackathonPonto.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HackathonPonto.Infra.Data.Mappings
{
    public class PerfisMap : IEntityTypeConfiguration<Perfil>
    {
        public void Configure(EntityTypeBuilder<Perfil> builder)
        {
            builder.ToTable("perfis");

            builder.HasKey(o => o.Id)
                .HasName("PRIMARY");

            builder.Property(o => o.Id)
                .HasColumnName("id");

            builder.Property(o => o.Nome)
                .HasColumnName("nome")
                .HasMaxLength(20);
        }
    }
}
