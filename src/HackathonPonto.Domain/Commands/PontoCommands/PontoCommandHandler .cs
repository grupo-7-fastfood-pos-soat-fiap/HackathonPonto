using HackathonPonto.Domain.Interfaces;
using HackathonPonto.Domain.Models;
using GenericPack.Messaging;
using MediatR;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

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

            var quantidadeRegistros = _repository.GetTotalRegistersDay(request.Data, request.FuncionarioId);

            if (quantidadeRegistros >= 4) {
                AddError("O limite de registro diário já foi atigindo");
                return CommandResult;
            }
            
            switch (quantidadeRegistros)
            {
                case 0: 
                    request.SetTipoRegistro("E1");
                    break;
                case 1:
                    request.SetTipoRegistro("S1");
                    break;
                case 2:
                    request.SetTipoRegistro("E2");
                    break;
                case 3:                          
                    request.SetTipoRegistro("S2");
                    break;
            }


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