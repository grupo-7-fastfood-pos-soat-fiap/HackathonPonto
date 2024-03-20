using GenericPack.Messaging;
using HackathonPonto.Domain.Commands.FuncionarioCommands.Validations;

namespace HackathonPonto.Domain.Commands.FuncionarioCommands
{
    public class FuncionarioDeleteCommand : FuncionarioCommand
    {
        protected FuncionarioDeleteCommand() { }

        public FuncionarioDeleteCommand(Guid id)
        {
            Id = id;
        }

        public override bool IsValid()
        {
            CommandResult.ValidationResult = new FuncionarioValidationsDelete().Validate(this);
            return CommandResult.ValidationResult.IsValid;
        }
    }
}
