using GenericPack.Domain;
using System;
using System.Collections.Generic;
namespace HackathonPonto.Domain.Models
{
    public class Usuario : Entity, IAggregateRoot
    {
        public string Login { get; private set; } = "";
        public string Senha { get; private set; } = "";

        public int PerfilId { get; private set; }
        public bool Ativo { get; private set; }

        public virtual Perfil? PerfilNavegation { get; private set; }

        private Usuario()
        {

        }

        public Usuario(string login, string senha, int perfilId, bool ativo)
        {
            Login = login;
            Senha = senha;
            PerfilId = perfilId;
            Ativo = ativo;
        }
    }
}
