using GenericPack.Domain;

namespace HackathonPonto.Domain.Models
{
    public partial class Ocupacao: Entity, IAggregateRoot
    {         
        public string Nome { get; private set; } = "";

        private Ocupacao() { 
            Funcionario = new HashSet<Funcionario>();
        }

        public Ocupacao(Guid id, string nome){
            Id = id;
            Nome = nome;         
            Funcionario = new HashSet<Funcionario>();   
        }

        public virtual ICollection<Funcionario> Funcionario { get; private set; } 
     
    }
}
