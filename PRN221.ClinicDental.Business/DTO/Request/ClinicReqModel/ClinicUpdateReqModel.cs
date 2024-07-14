using Microsoft.AspNetCore.Http;
using PRN221.ClinicDental.Business.Validate;
using PRN221.ClinicDental.Data.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PRN221.ClinicDental.Business.DTO.Request.ClinicReqModel
{
    public class ClinicUpdateReqModel
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public IFormFile ImageURL { get; set; }

        public string StreetAddress { get; set; }

        public string District { get; set; }

    }
}
