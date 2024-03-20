using GenericPack.Messaging;
using HackathonPonto.Domain.Commands.FuncionarioCommands.Validations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HackathonPonto.Domain.Commands.FuncionarioCommands
{
    public class FuncionarioUpdateCommand : FuncionarioCommand
    {
        protected FuncionarioUpdateCommand() { }

        public FuncionarioUpdateCommand(Guid id, string email, Guid ocupacaoId)
        {
            Id = id;
            Email = email;
            OcupacaoId = ocupacaoId;
        }

        public void SetId(Guid id)
        {
            Id = id;
        }

        public override bool IsValid()
        {
            CommandResult.ValidationResult = new FuncionarioValidationsUpdate().Validate(this);
            return CommandResult.ValidationResult.IsValid;
        }
    }
}
