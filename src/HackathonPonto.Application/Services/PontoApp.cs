using AutoMapper;
using GenericPack.Mediator;
using GenericPack.Messaging;
using HackathonPonto.Application.InputModels;
using HackathonPonto.Application.Interfaces;
using HackathonPonto.Application.ViewModels;
using HackathonPonto.Domain.Commands.FuncionarioCommands;
using HackathonPonto.Domain.Commands.PontoCommands;
using HackathonPonto.Domain.Interfaces;
using HackathonPonto.Infra.Data.Repository;

namespace HackathonPonto.Application.Services
{
    public class PontoApp : IPontoApp
    {
        private readonly IPontoRepository _pontoRepository;
        private readonly IFuncionarioRepository _funcionarioRepository;
        private readonly IMapper _mapper;
        private readonly IMediatorHandler _mediator;

        public PontoApp(IPontoRepository pontoRepository, IFuncionarioRepository funcionarioRepository, IMediatorHandler mediator, IMapper mapper)
        {
            _pontoRepository = pontoRepository;
            _funcionarioRepository = funcionarioRepository;
            _mediator = mediator;
            _mapper = mapper;
        }
        public async Task<CommandResult> Add(string cpf)
        {
            //#####################################################
            //TODO: Inserir regras de negócio
            //#####################################################
            var funcionario = _mapper.Map<FuncionarioViewModel>(_funcionarioRepository.GetByCpf(cpf));
            
            PontoInputModel model = new PontoInputModel();
            model.FuncionarioId = funcionario.Id;
            model.Data= DateOnly.FromDateTime(DateTime.Now);
            model.Hora= TimeOnly.FromDateTime(DateTime.Now);
            model.TipoRegistro = "E";

            var command = _mapper.Map<PontoCreateCommand>(model);
            return await _mediator.SendCommand(command);
        }

        public async Task<PontoViewModel> GetById(Guid id)
        {
            return _mapper.Map<PontoViewModel>(await _pontoRepository.GetById(id));
        }
        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }
  
    }
}
