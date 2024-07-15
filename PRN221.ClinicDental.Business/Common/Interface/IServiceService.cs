

using PRN221.ClinicDental.Business.DTO.Request.ServiceModel;
using PRN221.ClinicDental.Business.DTO.Response.Dentist;
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

        Task<ServiceViewAdminResponse> GetServiceByIdAsync(int id);
        Task<List<Service>> GetServiceByListIdAsync(List<int> ids);
        Task<List<ServiceResponseModel>> GetServicesByClinicId(int clinicId);
        Task<List<DentistResponseModel>> GetDentistsByServiceAndClinic(int serviceId, int clinicId);
        Task<ServiceResponseModel> GetServiceById(int serviceId);
        Task<List<ServiceViewAdminResponse>> GetAllServicesAdminView();
        Task<ServiceViewAdminResponse> CreateServiceAsync(ServiceViewAdminRequest service);
        Task UpdateServiceAsync(ServiceViewAdminResponse service);
        Task<bool> DeleteServiceAsync(int serviceId);

        Task<bool> ServiceNameExistsAsync(string serviceName);




    }
}
