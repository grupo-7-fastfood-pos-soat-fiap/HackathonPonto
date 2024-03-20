using HackathonPonto.Domain.Interfaces;
using HackathonPonto.Domain.Models;
using MediatR;

namespace HackathonPonto.Domain.Events.UsuarioEvents
{
    public class UsuarioEventHandler : INotificationHandler<UsuarioCreateEvent>
    {
        private readonly IUsuarioRepository _repository;
        public UsuarioEventHandler(IUsuarioRepository repository)
        {
            _repository = repository;
        }
        public Task Handle(UsuarioCreateEvent notification, CancellationToken cancellationToken)
        {
            var usuario = new Usuario(notification.Login, notification.Senha, notification.PerfilId,notification.Ativo);

            _repository.Add(usuario);

            return Task.CompletedTask;
        }
    }
}
