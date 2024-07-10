using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PRN221.ClinicDental.Data.Models
{
    public class DentistService
    {
        public int DentistId { get; set; }
        public DentistDetail Dentist { get; set; }

        public int ServiceId { get; set; }
        public Service Service { get; set; }
    }
}
