using HackathonPonto.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HackathonPonto.Infra.Data.Mappings
{
    public class PontosMap : IEntityTypeConfiguration<Ponto>
    {
        public void Configure(EntityTypeBuilder<Ponto> builder)
        {
            builder.ToTable("pontos");

            builder.HasKey(o => o.Id)
                .HasName("PRIMARY");

            builder.Property(o => o.Id)
                .HasColumnName("id");

            builder.Property(o => o.Data)
                .HasColumnName("data");

            builder.Property(o => o.Hora)
                .HasColumnName("hora");

            builder.Property(o => o.FuncionarioId)
                .HasColumnName("funcionario_id");

            builder.Property(o => o.TipoRegistro)
                .HasColumnName("tiporegistro");
        }       
    }
}
