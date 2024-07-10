using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace PRN221.ClinicDental.Business.Validate
{
    public class LettersOnlyAttribute : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            if (value != null)
            {
                string fullName = value.ToString();
                if (Regex.IsMatch(fullName, @"^[a-zA-Z\s]+$"))
                {
                    return ValidationResult.Success;
                }
                else
                {
                    return new ValidationResult("FullName must contain only letters.");
                }
            }
            return new ValidationResult("FullName is required.");
        }
    }
}
