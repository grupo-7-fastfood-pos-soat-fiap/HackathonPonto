using GenericPack.Data;
using HackathonPonto.Domain.Models;

namespace HackathonPonto.Domain.Interfaces
{
    public interface IPontoRepository : IRepository<Ponto>
    {
        Task<Ponto?> GetById(Guid id);
        void Add(Ponto ponto);
    }
}
