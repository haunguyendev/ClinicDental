using PRN221.ClinicDental.Business.Validate;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PRN221.ClinicDental.Business.DTO.Request.Appointment
{
    public class RescheduleAppointmentRequestModel
    {
        public int AppointmentId { get; set; }

        [Required]
        public DateTime NewDate { get; set; }

        [Required]
        public int NewSlot { get; set; }
    }
}
