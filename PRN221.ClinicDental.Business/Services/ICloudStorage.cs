using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PRN221.ClinicDental.Business.Services
{
    public interface ICloudStorage
    {
        public Task<string> UploadFile(IFormFile file, string filePath);
    }
}
