using PRN221.ClinicDental.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PRN221.ClinicDental.Business.DTO.Response.Clinic
{
    public class ClinicResponseModel
    {
        public int ClinicId { get; set; }
        public string ClinicName { get; set; }
        public string ImageURL { get; set; }
        public string StreetAddress { get; set; }
        public string District { get; set; }
        public string Services { get; set; }
    }
}
