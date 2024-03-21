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
using static System.Runtime.InteropServices.JavaScript.JSType;

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
            
            var funcionario = _mapper.Map<FuncionarioViewModel>(await _funcionarioRepository.GetByCpf(cpf));
            DateTime dataUnica = DateTime.Now;            

            PontoInputModel model = new PontoInputModel();
            model.FuncionarioId = funcionario.Id;
            model.Data= DateOnly.FromDateTime(dataUnica);
            model.Hora= TimeOnly.FromDateTime(dataUnica);
            model.TipoRegistro = "";

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

        public async Task<dynamic> GetDayByUser(DateOnly data, string cpf)
        {
            return await _pontoRepository.GetDayByUser(data, cpf);
        }

        public async Task<dynamic> GetMonthYearByUser(int mes, int ano, string cpf)
        {
            return await _pontoRepository.GetMonthYearByUser(mes, ano, cpf);
        }
    }    
}
