using AutoMapper;
using GenericPack.Mediator;
using GenericPack.Tools;
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
            var usuarioLogado = _mapper.Map<UsuarioViewModel>(await _usuarioRepository.GetByLogin(usuario.login));

            TokenViewModel result = new TokenViewModel();
            

            if (usuarioLogado == null || !usuarioLogado.Ativo || !Criptografia.CheckPassword(usuario.senha, usuarioLogado.Senha))
                return result;

            result.Token = "Bearer " + TokenService.GenerateToken(usuarioLogado.Login, usuarioLogado.Perfil.Nome);

            return result;
                       
        }
    }
}
