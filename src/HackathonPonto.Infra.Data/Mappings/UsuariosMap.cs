using HackathonPonto.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HackathonPonto.Infra.Data.Mappings
{
    public class UsuariosMap : IEntityTypeConfiguration<Usuario>
    {
        public void Configure(EntityTypeBuilder<Usuario> builder)
        {
            
                builder.ToTable("usuarios");

                builder.HasKey(o => o.Id)
                    .HasName("PRIMARY");

                builder.Property(o => o.Id)
                    .HasColumnName("id");

                builder.Property(o => o.Login)
                    .HasColumnName("login")
                    .HasMaxLength(11);

                builder.Property(o => o.Senha)
                    .HasColumnName("senha")
                    .HasMaxLength(248);

                builder.Property(o => o.PerfilId)
                    .HasColumnName("perfil_id");

                builder.Property(o => o.Ativo)
                    .HasColumnName("ativo");

            builder.HasIndex(f => f.PerfilId);

            builder.HasOne(f => f.PerfilNavegation)
                .WithMany(f => f.Usuario)
                .HasForeignKey(f => f.PerfilId);

            builder.Navigation(f => f.PerfilNavegation).AutoInclude();

        }
    }
}
