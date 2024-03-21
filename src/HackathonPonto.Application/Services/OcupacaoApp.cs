using AutoMapper;
using GenericPack.Mediator;
using HackathonPonto.Application.Interfaces;
using HackathonPonto.Application.ViewModels;
using HackathonPonto.Domain.Interfaces;

namespace HackathonPonto.Application.Services
{
    public class OcupacaoApp:IOcupacaoApp
    {
        private readonly IOcupacaoRepository _ocupacaoRepository;
        private readonly IMapper _mapper;
        private readonly IMediatorHandler _mediator;

        public OcupacaoApp(IOcupacaoRepository ocupacaoRepository, IMediatorHandler mediator, IMapper mapper)
        {
            _ocupacaoRepository = ocupacaoRepository;
            _mediator = mediator;
            _mapper = mapper;
        }

        public async Task<List<OcupacaoViewModel>> GetAll()
        {
            return _mapper.Map<List<OcupacaoViewModel>>(await _ocupacaoRepository.GetAll());
        }

        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }
    }
}
