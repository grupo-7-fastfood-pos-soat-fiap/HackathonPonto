﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HackathonPonto.Application.ViewModels
{
    public class UsuarioViewModel
    {
        public string Login { get; set; }
        public string Senha { get; set; }
        public PerfilViewModel Perfil { get; set; }
    }
}
