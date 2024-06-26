using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PRN221.ClinicDental.Business.DTO.Request.ClinicReqModel
{
    public  class ClinicReqModel
    {
        [Required]
        public string Name { get; set; }

        [Required]
        public string Address { get; set; }

        [Required]
        public IFormFile ImageURL { get; set; }

        [Required]
        public string StreetAddress {  get; set; }

        [Required]
        public string District {  get; set; }
    }
}
