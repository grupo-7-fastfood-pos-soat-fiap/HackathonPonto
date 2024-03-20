namespace HackathonPonto.Application.ViewModels
{
    public class PontoViewModel
    {
        public Guid Id { get; set; }
        public DateOnly Data { get; set; }
        public TimeOnly Hora { get; set; }
        public Guid FuncionarioId { get; set; }
        public string TipoRegistro { get; set; }

        private PontoViewModel()
        {

        }

        public PontoViewModel(Guid id, DateOnly data, TimeOnly hora, Guid funcionarioId, string tipoRegistro)
        {
            Id = id;
            Data = data;
            Hora = hora;
            FuncionarioId = funcionarioId;
            TipoRegistro = tipoRegistro;
        }
    }
}
