﻿using FluentValidation.Validators;

namespace HackathonPonto.Domain.CustomValidations
{
    public interface ICustomPropertyValidator
    {
        interface ICustomPropertyValidator : IPropertyValidator
        {
        }
    }
}
