using System.Collections.Generic;
using System.Threading.Tasks;
using PRN221.ClinicDental.Business.DTO.Response.Clinic;
using PRN221.ClinicDental.Data.Models;

namespace PRN221.ClinicDental.Services.Interfaces
{
    public interface IClinicService
    {
        Task<List<ClinicResponseModel>> GetClinicsByServiceId(int serviceId);
        Task<List<ClinicResponseModel>> GetAllClinic();
        Task<List<ClinicResponseModel>> SearchClinicByName(string keyword);


    }
}
