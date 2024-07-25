
﻿using PRN221.ClinicDental.Business.Validate;
using PRN221.ClinicDental.Data.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PRN221.ClinicDental.Business.DTO.Request.Dentist
{
    public class DentistReqModel
    {
        [Required]
        [StringLength(50)]
        [UsernameContainsLetter]
        public string Username { get; set; }

        [Required]
        [StringLength(255)]
        [PasswordComplexity]

        public string Password { get; set; }

        [Required]
        [StringLength(255)]
        public string FullName { get; set; }

        [Required]
        [EmailAddress]
        [StringLength(255)]
        public string Email { get; set; }

        [Required]
        [Phone]
        [StringLength(20)]
        public string PhoneNumber { get; set; }

        [Required]
        [StringLength(255)]
        public string Address { get; set; }

        [Required]
        [StringLength(255)]
        public string Certificate { get; set; }

        [Required]
        [StringLength(255)]
        public string Qualification { get; set; }

        [Required]
        [StringLength(255)]
        public string Experience { get; set; }

        [Required(ErrorMessage = "At least one service is required")]
        [MinLength(1, ErrorMessage = "At least one service is required")]
        public List<int> ServiceId { get; set; }
        public List<Service> DentistService { get; set; } = new List<Service>();
    }
}
