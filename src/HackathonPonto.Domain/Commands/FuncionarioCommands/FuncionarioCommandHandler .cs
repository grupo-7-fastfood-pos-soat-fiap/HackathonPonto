using HackathonPonto.Domain.Interfaces;
using HackathonPonto.Domain.Models;
using FluentValidation.Results;
using GenericPack.Messaging;
using MediatR;
using HackathonPonto.Domain.Events.UsuarioEvents;

namespace HackathonPonto.Domain.Commands.FuncionarioCommands
{
    public class FuncionarioCommandHandler : CommandHandler,
        IRequestHandler<FuncionarioCreateCommand, CommandResult>,
        IRequestHandler<FuncionarioUpdateCommand, CommandResult>,
        IRequestHandler<FuncionarioDeleteCommand, CommandResult>
    {

        private readonly IFuncionarioRepository _repository;
        public FuncionarioCommandHandler(IMediator mediator, IFuncionarioRepository repository)
        {
            _repository = repository;
        }

        public async Task<CommandResult> Handle(FuncionarioCreateCommand request, CancellationToken cancellationToken)
        {
            if (!request.IsValid()) return request.CommandResult;

            var funcionarioExiste = await _repository.GetByCpf(request.Cpf);
            if (funcionarioExiste is not null)
            {
                AddError("Já existe um funcionário com este CPF.");
                return CommandResult;
            }

            var funcionario = new Funcionario(Guid.NewGuid(), request.Nome, request.Matricula, request.Email, request.Cpf, request.OcupacaoId);

            funcionario.AddDomainEvent(new UsuarioCreateEvent(funcionario.Cpf));

            _repository.Add(funcionario);            

            return await Commit(_repository.UnitOfWork, funcionario.Id);
        }

        public async Task<CommandResult> Handle(FuncionarioUpdateCommand request, CancellationToken cancellationToken)
        {
            if (!request.IsValid()) return request.CommandResult;

            var funcionarioExiste = await _repository.GetById(request.Id);
            if (funcionarioExiste is null)
            {
                AddError("O Funcionário não existe.");
                return CommandResult;
            }

            var produto = new Funcionario(request.Id, funcionarioExiste.Nome, funcionarioExiste.Matricula, request.Email, funcionarioExiste.Cpf,request.OcupacaoId) ;

            _repository.Update(produto);

            return await Commit(_repository.UnitOfWork);
        }

        public async Task<CommandResult> Handle(FuncionarioDeleteCommand request, CancellationToken cancellationToken)
        {
            if (!request.IsValid()) return request.CommandResult;

            var funcionarioExiste = await _repository.GetById(request.Id);
            if (funcionarioExiste is null)
            {
                AddError("O Funcionário não existe.");
                return CommandResult;
            }

            _repository.Remove(funcionarioExiste);

            return await Commit(_repository.UnitOfWork);
        }
        public void Dispose()
        {
            _repository.Dispose();
        }
    }
}