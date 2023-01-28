using System;
using System.ComponentModel.DataAnnotations;
using Fba.Api.Exceptions;

namespace Fba.Api.Helper
{
	public class CustomFbaValidation : ValidationAttribute
    {
        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
        {
            if (value.ToString() == ValidationControlWorlds().FirstOrDefault())
            {
                throw new HbaBusinessException($"{validationContext.DisplayName} cannot be empty or null!");
            }

            if (!string.IsNullOrWhiteSpace(Convert.ToString(value)))
            {
                return ValidationResult.Success;
            }

            throw new HbaBusinessException($"{validationContext.DisplayName} cannot be empty or null!");
        }

        private IEnumerable<string> ValidationControlWorlds()
        {
            yield return "string";
            yield return "0";
            yield return "";
            yield return null;
        }
    }
}

