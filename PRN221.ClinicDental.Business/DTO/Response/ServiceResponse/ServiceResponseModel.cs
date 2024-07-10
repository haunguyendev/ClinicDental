using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PRN221.ClinicDental.Business.DTO.Response.ServiceResponse
{
    public class ServiceResponseModel
    {
        public int ServiceId { get; set; }

       
        public string ServiceName { get; set; } = null!;

        public decimal Price { get;set; }

        
        public string? Description { get; set; }
    }
}
