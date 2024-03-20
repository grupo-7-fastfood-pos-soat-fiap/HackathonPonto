using GenericPack.Domain;

namespace HackathonPonto.Domain.Models
{
    public class Funcionario: Entity, IAggregateRoot
    {   
        public string Nome { get; private set; }  = "";
        public string Matricula { get; private set; }  = "";
        public string Email { get; set; } = "";
        public string Cpf { get; set; } = "";
        public Guid OcupacaoId { get; private set;}

        public virtual Ocupacao? OcupacaoNavegation { get; private set; }
        
        public Funcionario(Guid id, string nome, string matricula, string email, string cpf, Guid ocupacaoId) 
        {
            Id = id;
            Nome = nome;            
            Matricula = matricula;
            Email = email;
            Cpf = cpf;
            OcupacaoId = ocupacaoId;
        }
    }
}
