using GenericPack.Messaging;

namespace HackathonPonto.Domain.Commands.PontoCommands
{
    public abstract class PontoCommand : Command
    {
        public Guid Id { get; protected set; }
        public DateOnly Data { get; protected set; }
        public TimeOnly Hora { get; protected set; }
        public Guid FuncionarioId { get; protected set; }
        public string TipoRegistro { get; protected set; } = string.Empty;

        public void SetTipoRegistro(string tipoRegistro)
        {
            TipoRegistro = tipoRegistro;
        }

    }
}