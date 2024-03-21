using HackathonPonto.Application.ViewModels;


namespace HackathonPonto.Application.Interfaces
{
    public interface IUsuarioApp:IDisposable
    {
        Task<List<UsuarioViewModel>> GetAll();
    }
}
