namespace HackathonPonto.Application.ViewModels
{
    public class FuncionarioViewModel
    {
        public Guid Id { get; set; }
        public string Nome { get; set; } = "";
        public string Matricula { get; set; } = "";
        public string Email { get; set; } = "";
        public string Cpf { get; set; } = "";
        public OcupacaoViewModel? Ocupacao { get; set; }


        public FuncionarioViewModel()
        {

        }

        public FuncionarioViewModel(Guid id, string nome, string matricula, string email, string cpf, OcupacaoViewModel? ocupacao)
        {
            Id = id;
            Nome = nome;
            Matricula = matricula;
            Email = email;
            Cpf = cpf;
            Ocupacao = ocupacao;
        }
    }
}