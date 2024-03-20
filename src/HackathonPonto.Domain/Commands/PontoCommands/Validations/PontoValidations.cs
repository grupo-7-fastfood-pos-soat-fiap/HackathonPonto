using FluentValidation;
using HackathonPonto.Domain.CustomValidations;

namespace HackathonPonto.Domain.Commands.PontoCommands.Validations
{
    public abstract class PontoValidations<T> : AbstractValidator<T> where T : PontoCommand
    {
        
    }
}