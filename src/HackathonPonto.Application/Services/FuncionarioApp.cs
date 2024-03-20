using AutoMapper;
using GenericPack.Mediator;
using GenericPack.Messaging;
using HackathonPonto.Application.InputModels;
using HackathonPonto.Application.Interfaces;
using HackathonPonto.Application.ViewModels;
using HackathonPonto.Domain.Commands.FuncionarioCommands;
using HackathonPonto.Domain.Interfaces;

namespace HackathonPonto.Application.Services
{
    public class FuncionarioApp : IFuncionarioApp
    {
        private readonly IFuncionarioRepository _funcionarioRepository;
        private readonly IMapper _mapper;
        private readonly IMediatorHandler _mediator;

        public FuncionarioApp(IFuncionarioRepository funcionarioRepository, IMediatorHandler mediator, IMapper mapper)
        {
            _funcionarioRepository = funcionarioRepository;
            _mediator = mediator;
            _mapper = mapper;
        }        

        public async Task<List<FuncionarioViewModel>> GetAll()
        {
            return _mapper.Map<List<FuncionarioViewModel>>(await _funcionarioRepository.GetAll());
        }

        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }

        public async Task<FuncionarioViewModel> GetById(Guid id)
        {
            return _mapper.Map<FuncionarioViewModel>(await _funcionarioRepository.GetById(id));
        }

        public async Task<FuncionarioViewModel> GetByEmail(string email)
        {
            return _mapper.Map<FuncionarioViewModel>(await _funcionarioRepository.GetByEmail(email));
        }

        public async Task<FuncionarioViewModel> GetByCpf(string cpf)
        {
            return _mapper.Map<FuncionarioViewModel>(await _funcionarioRepository.GetByCpf(cpf));
        }

        public async Task<CommandResult> Add(FuncionarioInputModel model)
        {
            var command = _mapper.Map<FuncionarioCreateCommand>(model);
            return await _mediator.SendCommand(command);
        }

        public async Task<CommandResult> Update(Guid id, FuncionarioInputModel model)
        {
            var command = _mapper.Map<FuncionarioUpdateCommand>(model);
            command.SetId(id);
            return await _mediator.SendCommand(command);
        }

        public async Task<CommandResult> Remove(Guid id)
        {
            var command = new FuncionarioDeleteCommand(id);
            return await _mediator.SendCommand(command);
        }
    }
}