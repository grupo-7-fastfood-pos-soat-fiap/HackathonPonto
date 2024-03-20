using GenericPack.Data;
using HackathonPonto.Domain.Interfaces;
using HackathonPonto.Domain.Models;
using HackathonPonto.Infra.Data.Context;
using Microsoft.EntityFrameworkCore;

namespace HackathonPonto.Infra.Data.Repository
{
    public class FuncionarioRepository: IFuncionarioRepository
    {
        protected readonly AppDbContext Db;
        protected readonly DbSet<Funcionario> DbSet;

        public FuncionarioRepository(AppDbContext context)
        {
            Db = context;
            DbSet = Db.Set<Funcionario>();
        }
        public IUnitOfWork UnitOfWork => Db;

        public async Task<IEnumerable<Funcionario>> GetAll()
        {
            return await DbSet.AsNoTracking().OrderBy(on => on.Nome).ToListAsync();
        }
        
        public async Task<Funcionario?> GetById(Guid id)
        {
            return await DbSet.AsNoTracking().FirstAsync(x => x.Id == id);
        }

        public void Add(Funcionario funcionario)
        {
            DbSet.Add(funcionario);        
        }

        public void Remove(Funcionario funcionario)
        {
            DbSet.Remove(funcionario);
        }

        public void Update(Funcionario funcionario)
        {
            DbSet.Update(funcionario);          
        }

        public void Dispose()
        {
            Db.Dispose();
        }

        public async Task<Funcionario?> GetByEmail(string email)
        {
            return await DbSet.FirstOrDefaultAsync(f => f.Email == email);
        }

        public async Task<Funcionario?> GetByCpf(string cpf)
        {
            return await DbSet.FirstOrDefaultAsync(f => f.Cpf == cpf);            
        }
    }
}
