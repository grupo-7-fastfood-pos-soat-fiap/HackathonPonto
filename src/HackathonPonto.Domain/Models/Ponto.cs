
using GenericPack.Domain;

namespace HackathonPonto.Domain.Models
{
    public partial class Ponto : Entity, IAggregateRoot
    {
        public DateOnly Data { get; private set; }
        public TimeOnly Hora { get; private set; }
        public Guid FuncionarioId { get; private set; }
        public string TipoRegistro { get; private set; }
        
        private Ponto()
        {
            //Funcionario = new HashSet<Funcionario>();
        }

        public Ponto(Guid id, DateOnly data, TimeOnly hora, Guid funcionarioId, string tipoRegistro)
        {
            Id = id;            
            Data = data;
            Hora = hora;
            FuncionarioId = funcionarioId;
            TipoRegistro = tipoRegistro;

            //Funcionario = new HashSet<Funcionario>();
        }

        //public virtual ICollection<Funcionario> Funcionario { get; private set; }
    }
}
