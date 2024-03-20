
namespace HackathonPonto.Domain.Commands.FuncionarioCommands.Validations
{
    public class FuncionarioValidationsDelete : FuncionarioValidations<FuncionarioDeleteCommand>
    {
        public FuncionarioValidationsDelete()
        {
            ValidaId();            
        }
    }
}
