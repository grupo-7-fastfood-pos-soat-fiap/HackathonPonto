using GenericPack.Messaging;
using HackathonPonto.Application.InputModels;
using HackathonPonto.Application.ViewModels;

namespace HackathonPonto.Application.Interfaces
{
    public interface IFuncionarioApp : IDisposable
    {
        Task<List<FuncionarioViewModel>> GetAll();
        Task<FuncionarioViewModel> GetById(Guid id);
        Task<FuncionarioViewModel> GetByEmail(string email);
        Task<FuncionarioViewModel> GetByCpf(string cpf);
        Task<CommandResult> Add(FuncionarioInputModel model);
        Task<CommandResult> Update(Guid id, FuncionarioInputModel model);
        Task<CommandResult> Remove(Guid id);
    }
}