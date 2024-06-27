using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PRN221.ClinicDental.Business.DTO.Request.Appointment
{
    public class AppointmentRequestModel
    {
        [Required]
        public int ServiceId { get; set; }

        [Required]
        public int ClinicId { get; set; }

        [Required]
        public int DentistId { get; set; }

        [Required]
        [DataType(DataType.Date)]
        public DateTime AppointmentDate { get; set; }
        [Required]
        public int Slot { get; set; }

        [StringLength(20)]
        [Phone]
        public string? PhoneNumber { get; set; }

        [StringLength(255)]
        public string? Notes { get; set; }

      

    }
}
