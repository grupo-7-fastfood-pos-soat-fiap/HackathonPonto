using HackathonPonto.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HackathonPonto.Infra.Data.Mappings
{
    public class FuncionariosMap: IEntityTypeConfiguration<Funcionario>
    {
        public void Configure(EntityTypeBuilder<Funcionario> builder)
        {
            builder.ToTable("funcionarios");
   
            builder.HasKey(f => f.Id)
                .HasName("PRIMARY");

            builder.Property(f => f.Id)
                .HasColumnName("id");

            builder.Property(f => f.Nome)
                .HasColumnName("nome")
                .HasMaxLength(100);

            builder.Property(f => f.Matricula)
                .HasColumnName("matricula")
                .HasMaxLength(15);

            builder.Property(f => f.Email)
                .HasColumnName("email")
                .HasMaxLength(80);

            builder.Property(f => f.Cpf)
                .HasColumnName("cpf")
                .HasMaxLength(11);

            builder.Property(f => f.OcupacaoId)
                .HasColumnName("ocupacao_id");  
            
            builder.HasIndex(f => f.OcupacaoId);

            builder.HasOne(f => f.OcupacaoNavegation)
                .WithMany(f => f.Funcionario)
                .HasForeignKey(f => f.OcupacaoId);            
            
            builder.Navigation(f => f.OcupacaoNavegation).AutoInclude();
        }
    }
}
