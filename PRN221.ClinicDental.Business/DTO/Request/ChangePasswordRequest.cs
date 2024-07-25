using PRN221.ClinicDental.Business.Validate;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PRN221.ClinicDental.Business.DTO.Request
{
    public class ChangePasswordRequest
    {
        [Required]
        public string CurrentPassword { get; set; }
        [Required]
        [StringLength(255)]
        [PasswordComplexity]
        public string NewPassword { get; set; }
        [Required]
        [Compare("NewPassword")]
        [StringLength(255)]
        public string ConfirmPassword { get; set; }
    }
}
