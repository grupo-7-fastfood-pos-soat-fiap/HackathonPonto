using HackathonPonto.Application.ViewModels;
using System.ComponentModel.DataAnnotations;

namespace HackathonPonto.Application.InputModels
{
    public class FuncionarioInputModel
    {
        [Required(ErrorMessage = "O nome do funcionário é requerido.")]
        [StringLength(100, ErrorMessage = "O nome deve ter no máximo 100 caracteres")]
        public string Nome { get; set; } = "";

        [Required(ErrorMessage = "A matrícula do funcionário é requerida.")]
        [StringLength(15, ErrorMessage = "A matrícula deve ter no máximo 15 caracteres")]
        public string Matricula { get; set; } = "";

        [Required(ErrorMessage = "O E-mail do funcionário é requerido.")]
        [EmailAddress(ErrorMessage = "E-mail com formato inválido.")]
        public string Email { get; set; } = "";

        [Required(ErrorMessage = "O CPF do funcionário é requerido.")]
        [StringLength(11, ErrorMessage = "O CPF deve ter no máximo 11 dígitos")]        
        public string Cpf { get; set; } = "";

        [Required(ErrorMessage = "A ocupação do funcionário é requerida.")]
        public Guid  OcupacaoId { get; set; }
    }
}
