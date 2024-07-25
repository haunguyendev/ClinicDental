using Microsoft.AspNetCore.Http;
using PRN221.ClinicDental.Business.Validate;
using PRN221.ClinicDental.Data.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
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
        [AllowedExtensions(new string[] { ".jpg", ".png" })]
        public IFormFile ImageURL { get; set; }

        [Required]
        public string StreetAddress {  get; set; }

        [Required]
        public string District {  get; set; }

        [Required(ErrorMessage = "At least one service is required")]
        [MinLength(1, ErrorMessage = "At least one service is required")]
        public List<int> ServiceId { get; set; }

        public List<Service> ClinicServices { get; set; } = new List<Service>();

    }
}
