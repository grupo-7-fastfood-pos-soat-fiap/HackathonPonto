using AutoMapper;
using GenericPack.Mediator;
using HackathonPonto.Application.Interfaces;
using HackathonPonto.Application.ViewModels;
using HackathonPonto.Domain.Interfaces;

namespace HackathonPonto.Application.Services
{
    public class UsuarioApp : IUsuarioApp
    {
        private readonly IUsuarioRepository _usuarioRepository;
        private readonly IMapper _mapper;
        private readonly IMediatorHandler _mediator;

        public UsuarioApp(IUsuarioRepository usuarioRepository, IMediatorHandler mediator, IMapper mapper)
        {
            _usuarioRepository = usuarioRepository;
            _mediator = mediator;
            _mapper = mapper;
        }

        public async Task<List<UsuarioViewModel>> GetAll()
        {
            return _mapper.Map<List<UsuarioViewModel>>(await _usuarioRepository.GetAll());
        }

        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }
    }
}
