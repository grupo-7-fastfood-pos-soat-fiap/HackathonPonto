using AutoMapper;
using GenericPack.Mediator;
using HackathonPonto.Application.InputModels;
using HackathonPonto.Application.Interfaces;
using HackathonPonto.Application.ViewModels;
using HackathonPonto.Domain.Interfaces;
using HackathonPonto.Infra.Data.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HackathonPonto.Application.Services
{
    public class LoginApp : ILoginApp
    {
        private readonly IUsuarioRepository _usuarioRepository;
        private readonly IMapper _mapper;
        private readonly IMediatorHandler _mediator;

        public LoginApp(IUsuarioRepository usuarioRepository, IMediatorHandler mediator, IMapper mapper)
        {
            _usuarioRepository = usuarioRepository;
            _mediator = mediator;
            _mapper = mapper;
        }

        public async Task<TokenViewModel> Autenticar(UsuarioInputModel usuario)
        {
            var usarioLogado = _mapper.Map<UsuarioViewModel>(await _usuarioRepository.GetByLogin(usuario.login));

            TokenViewModel result = new TokenViewModel();

            if (usarioLogado == null || usarioLogado.Senha != usuario.senha)
                return result;

            result.Token = "Bearer " + TokenService.GenerateToken(usarioLogado.Login, usarioLogado.Perfil.Nome);

            return result;
                       
        }
    }
}
