using GenericPack.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HackathonPonto.Domain.Models
{
    public class Perfil: IAggregateRoot
    {
        public int Id { get; private set; }
        public string Nome { get; private set; }

        private Perfil() {
            Usuario = new HashSet<Usuario>();
        }

        public Perfil(int id, string nome)
        {
            Id = id;
            Nome = nome;

            Usuario = new HashSet<Usuario>();
        }

        public virtual ICollection<Usuario> Usuario { get; private set; }
    }
}
