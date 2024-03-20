namespace HackathonPonto.Domain.Commands.FuncionarioCommands.Validations
{
    public class FuncionarioValidationsCreate:FuncionarioValidations<FuncionarioCreateCommand>
    {
        public FuncionarioValidationsCreate(){            
            ValidaNome();
            ValidaMatricula();
            ValidaEmail();
            ValidaCpf();            
        }
    }
}