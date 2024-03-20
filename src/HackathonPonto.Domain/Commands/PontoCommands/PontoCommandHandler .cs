using HackathonPonto.Domain.Interfaces;
using HackathonPonto.Domain.Models;
using FluentValidation.Results;
using GenericPack.Messaging;
using MediatR;

namespace HackathonPonto.Domain.Commands.PontoCommands
{
    public class PontoCommandHandler : CommandHandler,
        IRequestHandler<PontoCreateCommand, CommandResult>
    {

        private readonly IPontoRepository _repository;
        public PontoCommandHandler(IMediator mediator, IPontoRepository repository)
        {
            _repository = repository;
        }

        public async Task<CommandResult> Handle(PontoCreateCommand request, CancellationToken cancellationToken)
        {
            if (!request.IsValid()) return request.CommandResult;            
            
            var Ponto = new Ponto(Guid.NewGuid(), request.Data,request.Hora, request.FuncionarioId, request.TipoRegistro);            
            
            _repository.Add(Ponto);            

            return await Commit(_repository.UnitOfWork, Ponto.Id);
        }
       
        public void Dispose()
        {
            _repository.Dispose();
        }
    }
}