using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;

namespace AirbnbProject.Models.Validation
{
    public class BathroomsAttribute : ValidationAttribute, IClientModelValidator
    {
        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
        {
            if (value == null) return new ValidationResult("Bathrooms is required.");

            if (!decimal.TryParse(value.ToString(), out var dec)) return new ValidationResult("Bathrooms must be a number.");
            var intPart = Math.Truncate(dec);
            var frac = dec - intPart;

            if (frac == 0m) return ValidationResult.Success;
            if (frac == 0.5m) return ValidationResult.Success;

            return new ValidationResult("Bathrooms must be integer or end with .5 (e.g., 1.5).");
        }

        public void AddValidation(ClientModelValidationContext context)
        {
            if (!context.Attributes.ContainsKey("data-val"))
                context.Attributes.Add("data-val", "true");

            context.Attributes.Add("data-val-bathrooms", ErrorMessage ?? "Bathrooms value invalid.");
        }
    }
}
