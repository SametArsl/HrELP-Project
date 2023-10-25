using System.ComponentModel.DataAnnotations;

namespace HrELP.Application.Extensions
{
    public class LetterValidator : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            if (value != null)
            {
                string input = value.ToString();
                if (!string.IsNullOrEmpty(input) && !input.All(char.IsLetter))
                {
                    return new ValidationResult(ErrorMessage ?? "Input must contain only letters.");
                }
            }
            return ValidationResult.Success;
        }
    }
}
