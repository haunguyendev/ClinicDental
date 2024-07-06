using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PRN221.ClinicDental.Business.DTO.Response.Appointment
{
    public class AppointmentResponseModel
    {
        public int AppointmentId { get; set; }
        public string DentistName { get; set; }
        public DateTime AppointmentDate { get; set; }
        public string ClinicName { get; set; }
        public string ServiceName { get; set; }
        public string TimeRange { get; set; }
        public string Address { get; set; }
        public string Status { get; set; }
        public string PhoneNumber { get; set; }
        public string Notes { get; set; }
    }
}
