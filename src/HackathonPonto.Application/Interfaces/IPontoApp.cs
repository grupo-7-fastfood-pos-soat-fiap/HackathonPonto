using GenericPack.Messaging;
using HackathonPonto.Application.InputModels;
using HackathonPonto.Application.ViewModels;

namespace HackathonPonto.Application.Interfaces
{
    public interface IPontoApp : IDisposable
    {
        Task<CommandResult> Add(Guid funcionarioId);
        Task<PontoViewModel> GetById(Guid id);
    }
}
