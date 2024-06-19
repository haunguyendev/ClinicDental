using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PRN221.ClinicDental.Business.DTO.Response
{
    public class UserLoginResponse
    {
       
        public int UserId { get; set; }

        public string Username { get; set; } = null!;

        public string Name { get; set; } = null!;

        public string? Email { get; set; }

     
        public string? Phone { get; set; }

        public string? Address { get; set; }
        public string Role { get; set; }
    }
}
