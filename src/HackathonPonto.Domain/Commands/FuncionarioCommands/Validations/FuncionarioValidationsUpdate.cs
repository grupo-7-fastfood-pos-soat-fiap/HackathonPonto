namespace HackathonPonto.Domain.Commands.FuncionarioCommands.Validations
{
    public class FuncionarioValidationsUpdate : FuncionarioValidations<FuncionarioUpdateCommand>
    {
        public FuncionarioValidationsUpdate()
        {
            ValidaEmail();            
        }
    }
}
