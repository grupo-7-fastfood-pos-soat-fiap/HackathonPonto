using GenericPack.Messaging;

namespace HackathonPonto.Domain.Commands.FuncionarioCommands
{
    public abstract class FuncionarioCommand : Command
    {
        public Guid Id { get; protected set; }
        public string Nome { get; protected set; } = "";
        public string Matricula { get; protected set; } = "";
        public string Email { get; protected set; } = "";
        public string Cpf { get; protected set; } = "";

        public Guid OcupacaoId { get; protected set; }
    }
}