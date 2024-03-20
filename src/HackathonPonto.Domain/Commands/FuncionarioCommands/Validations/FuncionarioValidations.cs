using FluentValidation;
using HackathonPonto.Domain.CustomValidations;

namespace HackathonPonto.Domain.Commands.FuncionarioCommands.Validations
{
    public abstract class FuncionarioValidations<T> : AbstractValidator<T> where T : FuncionarioCommand
    {
        protected void ValidaId()
        {
            RuleFor(f => f.Id)
                .NotEqual(Guid.Empty)
                .WithMessage("O Id do funcionário não foi informado");
        }

        protected void ValidaNome()
        {
            RuleFor(f => f.Nome)
                .NotEmpty()
                .WithMessage("O Nome do funcionário é requerido");

            RuleFor(f => f.Nome)
                .Length(3, 100)
                .When(c => c.Nome != null)
                .WithMessage("O Nome do funcionário deve ter entre 3 e 100 caracteres");            
        }

        protected void ValidaMatricula()
        {
            RuleFor(f => f.Matricula)
                .NotEmpty()
                .WithMessage("A Matrícula do funcionário é requerida");            
        }

        protected void ValidaEmail()
        {
            RuleFor(f => f.Email)
                .NotEmpty()
                .WithMessage("O E-mail é requerido");

            RuleFor(f => f.Email) 
                .EmailAddress()                                                           
                .When(c => c.Email != null)                 
                .WithMessage("O E-mail não possui um formato válido");
        }

        protected void ValidaCpf()
        {
            RuleFor(f => f.Cpf)
                .NotEmpty()
                .WithMessage("O CPF do funcionário é requerido");

            RuleFor(f => f.Cpf)
                .IsValidCPF()
                .When(f => f.Cpf != string.Empty)
                .WithMessage("O CPF não é válido");

            
        }

    }
}