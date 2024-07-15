using PRN221.ClinicDental.Data.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PRN221.ClinicDental.Business.DTO.Request.Dentist
{
    public class DentistUpdateReqModel
    {
        public int ID { get; set; }
        [Required]
        [StringLength(255)]
        public string FullName { get; set; }

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
        public List<DentistService> DentistService { get; set; } = new List<DentistService>();
    }
}
