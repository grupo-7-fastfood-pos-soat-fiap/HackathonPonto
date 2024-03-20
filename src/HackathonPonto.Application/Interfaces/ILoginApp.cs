using HackathonPonto.Application.InputModels;
using HackathonPonto.Application.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HackathonPonto.Application.Interfaces
{
    public interface ILoginApp
    {
        Task<TokenViewModel> Autenticar(UsuarioInputModel usuario);
    }
}
