using GenericPack.Data;
using HackathonPonto.Domain.Models;

namespace HackathonPonto.Domain.Interfaces
{
    public interface IFuncionarioRepository: IRepository<Funcionario>
    {
        Task<Funcionario?> GetById(Guid id);
        Task<Funcionario?> GetByEmail(string email);
        Task<Funcionario?> GetByCpf(string cpf);
        Task<IEnumerable<Funcionario>> GetAll();
        void Add(Funcionario funcionario);
        void Update(Funcionario funcionario);
        void Remove(Funcionario funcionario);
    }
}
