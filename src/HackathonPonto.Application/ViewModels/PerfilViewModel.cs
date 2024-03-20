using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HackathonPonto.Application.ViewModels
{
    public class PerfilViewModel
    {
        public int Id { get; set; }
        public string Nome { get; set; } = "";

        public PerfilViewModel() { }

        public PerfilViewModel(int id, string nome)
        {
            Id = id;
            Nome = nome;
        }
    }
}
