using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PRN221.ClinicDental.Business.Validate
{
    public class ValidPhoneAttribute : RegularExpressionAttribute
    {
        public ValidPhoneAttribute() : base(@"^(\+[0-9]{9,15}|[0-9]{10,15})$")
        {
            ErrorMessage = "Invalid phone number.";
        }
    }
}
