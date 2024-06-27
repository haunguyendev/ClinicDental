using PRN221.ClinicDental.Business.DTO.Response.ServiceResponse;
using PRN221.ClinicDental.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PRN221.ClinicDental.Business.Common.Interface
{
    public interface IServiceService
    {
        Task<List<ServiceResponseModel>> GetAllListServices();
        Task<ServiceResponseModel> GetServiceByIdAsync(int id);
        Task<List<Service>> GetServiceByListIdAsync(List<int> ids);
    }
}
