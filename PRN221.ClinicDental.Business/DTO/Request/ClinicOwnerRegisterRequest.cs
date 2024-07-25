using PRN221.ClinicDental.Business.Validate;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PRN221.ClinicDental.Business.DTO.Request
{
    public class ClinicOwnerRegisterRequest
    {
        [Required]
        [StringLength(50)]
        [UsernameContainsLetter]
        public string Username { get; set; }

        [Required]
        [StringLength(255)]
        [LettersOnly]
        public string FullName { get; set; }

        [EmailAddress]
        [StringLength(255)]
        public string Email { get; set; }

        [Phone]
        [StringLength(20)]
        public string PhoneNumber { get; set; }

        [StringLength(255)]
        public string Address { get; set; }

    }
}
