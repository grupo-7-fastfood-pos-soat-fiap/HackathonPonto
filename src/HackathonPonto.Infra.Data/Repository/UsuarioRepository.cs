
using GenericPack.Data;
using HackathonPonto.Domain.Interfaces;
using HackathonPonto.Domain.Models;
using HackathonPonto.Infra.Data.Context;
using Microsoft.EntityFrameworkCore;

namespace HackathonPonto.Infra.Data.Repository
{
    public class UsuarioRepository : IUsuarioRepository
    {
        protected readonly AppDbContext Db;
        protected readonly DbSet<Usuario> DbSet;

        public UsuarioRepository(AppDbContext context)
        {
            Db = context;
            DbSet = Db.Set<Usuario>();
        }

        public IUnitOfWork UnitOfWork => Db;

        public void Add(Usuario usuario)
        {
            DbSet.Add(usuario);
        }

        public void Dispose()
        {
            Db.Dispose();
        }

        public async Task<IEnumerable<Usuario>> GetAll()
        {
            return await DbSet.AsNoTracking().OrderBy(x => x.Login).ToListAsync();
        }

        public async Task<IEnumerable<Usuario>> GetAllByPerfil(int perfilId)
        {
            return await DbSet.AsNoTracking().Where(x => x.PerfilId == perfilId).OrderBy(x => x.Login).ToListAsync();
        }

        public async Task<Usuario?> GetByLogin(string login)
        {
            return await DbSet.FirstOrDefaultAsync(x => x.Login == login);
        }

        public void Remove(Usuario usuario)
        {
            throw new NotImplementedException();
        }

        public void Update(Usuario usuario)
        {
            throw new NotImplementedException();
        }
    }
}
