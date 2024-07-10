using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace PRN221.ClinicDental.Business.Validate
{
    public class PasswordComplexityAttribute : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            if (value != null)
            {
                string password = value.ToString();
                if (Regex.IsMatch(password, @"^(?=.*[a-zA-Z])(?=.*\d)(?=.*[@$!%*?&#])[A-Za-z\d@$!%*?&#]{8,}$"))
                {
                    return ValidationResult.Success;
                }
                else
                {
                    return new ValidationResult("Password must contain at least one letter, one number, and one special character and length must be above 8 letter");
                }
            }
            return new ValidationResult("Password is required.");
        }
    }
}
