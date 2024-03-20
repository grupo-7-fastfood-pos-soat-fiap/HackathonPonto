using System.ComponentModel.DataAnnotations;

namespace HackathonPonto.Application.InputModels
{
    public class PontoInputModel
    {                
        public DateOnly Data { get; set; }
        public TimeOnly Hora { get; set; }
        public Guid FuncionarioId { get; set; }
        public string TipoRegistro { get; set; }
    }
}
