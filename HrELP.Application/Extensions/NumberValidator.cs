using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HrELP.Application.Extensions
{
    public class NumberValidator: ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            if (value != null)
            {
                string input = value.ToString();
                if (!string.IsNullOrEmpty(input) && !input.All(char.IsDigit))
                {
                    return new ValidationResult(ErrorMessage ?? "Input must contain only numbers.");
                }
            }
            return ValidationResult.Success;
        }
    }
}
