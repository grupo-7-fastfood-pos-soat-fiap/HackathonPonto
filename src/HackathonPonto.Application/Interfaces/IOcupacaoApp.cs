using HackathonPonto.Application.ViewModels;

namespace HackathonPonto.Application.Interfaces
{
    public interface IOcupacaoApp:IDisposable
    {
        Task<List<OcupacaoViewModel>> GetAll();
    }
}
