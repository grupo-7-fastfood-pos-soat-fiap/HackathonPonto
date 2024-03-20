using GenericPack.Data;
using HackathonPonto.Domain.Models;

namespace HackathonPonto.Domain.Interfaces
{
    public interface IUsuarioRepository : IRepository<Usuario>
    {
        Task<Usuario?> GetByLogin(string login);
        Task<IEnumerable<Usuario>> GetAll();
        Task<IEnumerable<Usuario>> GetAllByPerfil(int perfilId);
        void Add(Usuario usuario);
        void Update(Usuario usuario);
        void Remove(Usuario usuario);
    }
}
