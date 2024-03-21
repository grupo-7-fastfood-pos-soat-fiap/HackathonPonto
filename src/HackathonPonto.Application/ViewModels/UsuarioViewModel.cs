using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace HackathonPonto.Application.ViewModels
{
    public class UsuarioViewModel
    {
        public string Login { get; set; }
        [XmlIgnore]
        [JsonIgnore]
        public string Senha { get; set; }
        public bool Ativo {  get; set; }
        public PerfilViewModel Perfil { get; set; }
    }
}
