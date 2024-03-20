using HackathonPonto.Domain.Commands.FuncionarioCommands.Validations;

namespace HackathonPonto.Domain.Commands.FuncionarioCommands
{
    public class FuncionarioCreateCommand : FuncionarioCommand
    {
        protected FuncionarioCreateCommand(){}

        public FuncionarioCreateCommand(string nome, string matricula, string email, string cpf, Guid ocupacaoId){
            Nome=nome;
            Matricula=matricula;
            Email=email;
            Cpf=cpf;
            OcupacaoId=ocupacaoId;
        }

        public override bool IsValid()
        {
            CommandResult.ValidationResult = new FuncionarioValidationsCreate().Validate(this);
            return CommandResult.ValidationResult.IsValid;
        }
    }
}