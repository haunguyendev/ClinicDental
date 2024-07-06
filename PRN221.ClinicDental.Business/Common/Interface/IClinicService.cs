using System.Collections.Generic;
using System.Threading.Tasks;
using PRN221.ClinicDental.Business.DTO.Request.ClinicReqModel;
using PRN221.ClinicDental.Business.DTO.Response.Clinic;
using PRN221.ClinicDental.Business.Helper;
using PRN221.ClinicDental.Data.Models;

namespace PRN221.ClinicDental.Services.Interfaces
{
    public interface IClinicService
    {
        Task<List<ClinicResponseModel>> GetClinicsByServiceId(int serviceId);
        Task<PaginatedList<ClinicResponseModel>> GetAllClinic(int pageNumber, int pageSize);
        Task<PaginatedList<ClinicResponseModel>> SearchClinicByName(string keyword, int pageNumber, int pageSize);
        Task<List<DistrictGroupModel>> GetClinicsGroupedByDistrict();
        Task<List<Clinic>> GetClinicsByClinicOwnerId(int userId);
        Task <bool> AddClinic (ClinicReqModel clinic, int customerId);
        Task<PaginatedList<ClinicResponseModel>> GetClinicsByDistrict(string district, int pageNumber, int pageSize);
    }
}
