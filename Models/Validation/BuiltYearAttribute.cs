using System;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;

namespace Airbnb.Models.Validation
{
    public class BuiltYearAttribute : ValidationAttribute, IClientModelValidator
    {
        private readonly int _maxYears;

        public BuiltYearAttribute(int maxYears)
        {
            _maxYears = maxYears;
        }

        public override bool IsValid(object value)
        {
            if (value == null) return false;

            int year = (int)value;
            int currentYear = DateTime.Now.Year;

            if (year >= currentYear) return false;

            int oldestAllowed = currentYear - _maxYears;
            return year >= oldestAllowed;
        }

        public void AddValidation(ClientModelValidationContext context)
        {
            context.Attributes["data-val"] = "true";
            context.Attributes["data-val-builtyear"] =
                ErrorMessage ?? $"Built year must be in the past and not older than {_maxYears} years.";

            // This MUST match JS addSingleVal("builtyear", "maxyears")
            context.Attributes["data-val-builtyear-maxyears"] = _maxYears.ToString();
        }
    }
}
