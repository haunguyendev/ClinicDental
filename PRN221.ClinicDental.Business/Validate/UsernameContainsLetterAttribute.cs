using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace PRN221.ClinicDental.Business.Validate
{
    public class UsernameContainsLetterAttribute : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            if (value != null)
            {
                string username = value.ToString();
                if (Regex.IsMatch(username, "[a-zA-Z]"))
                {
                    return ValidationResult.Success;
                }
                else
                {
                    return new ValidationResult("Username must contain at least one letter.");
                }
            }
            return new ValidationResult("Username is required.");
        }
    }
}
