using HackathonPonto.Domain.Commands.PontoCommands.Validations;

namespace HackathonPonto.Domain.Commands.PontoCommands
{
    public class PontoCreateCommand : PontoCommand
    {
        protected PontoCreateCommand(){}

        public PontoCreateCommand(DateOnly data, TimeOnly hora, Guid funcionarioId, string tipoRegistro){
            Data = data;
            Hora = hora;
            FuncionarioId=funcionarioId;
            TipoRegistro= tipoRegistro;
        }

        public override bool IsValid()
        {
            CommandResult.ValidationResult = new PontoValidationsCreate().Validate(this);
            return CommandResult.ValidationResult.IsValid;
        }
    }
}