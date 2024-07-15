using PRN221.ClinicDental.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PRN221.ClinicDental.Data.Common.Interface
{
    public interface IServiceRepository:IRepositoryBase<Service>
    {
        Task<List<Service>> GetAllServices();
        Task<Service> GetServiceById(int id);
        Task<List<Service>> GetServiceByListId(List<int> ids);
        Task<List<ClinicService>> GetServicesByClinicId(int clinicId);
        Task<List<DentistDetail>> GetDentistsByServiceAndClinic(int serviceId, int clinicId);
        Task<bool> ServiceNameExistsAsync(string serviceName);

    }
}
