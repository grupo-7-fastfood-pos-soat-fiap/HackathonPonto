﻿using GenericPack.Messaging;
using GenericPack.Tools;
using HackathonPonto.Domain.Models;

namespace HackathonPonto.Domain.Events.UsuarioEvents
{
    public class UsuarioCreateEvent: Event
    {
        public string Login { get; protected set; } = "";
        public string Senha { get; protected set; } = "";

        public int PerfilId { get; protected set; }
        public bool Ativo { get; protected set; }

        private UsuarioCreateEvent()
        {

        }

        public UsuarioCreateEvent(string login)
        {
            Login = login;
            Senha = Criptografia.EncodePassword("1234");
            PerfilId = (int)Models.Enums.Perfil.Colaborador;
            Ativo = true;
        }
    }
}
