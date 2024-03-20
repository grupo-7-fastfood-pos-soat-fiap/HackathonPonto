using GenericPack.Data;
using HackathonPonto.Domain.Interfaces;
using HackathonPonto.Domain.Models;
using HackathonPonto.Infra.Data.Context;
using Microsoft.EntityFrameworkCore;

namespace HackathonPonto.Infra.Data.Repository
{
    public class PontoRepository : IPontoRepository
    {
        protected readonly AppDbContext Db;
        protected readonly DbSet<Ponto> DbSet;

        public PontoRepository(AppDbContext context)
        {
            Db = context;
            DbSet = Db.Set<Ponto>();
        }

        public IUnitOfWork UnitOfWork => Db;

        public void Add(Ponto ponto)
        {
            DbSet.Add(ponto);
        }

        public void Dispose()
        {
            Db.Dispose();
        }

        public async Task<Ponto?> GetById(Guid id)
        {
            return await DbSet.FindAsync(id);
        }
    }
}
