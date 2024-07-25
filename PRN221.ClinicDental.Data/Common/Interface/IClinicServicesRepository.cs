using PRN221.ClinicDental.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PRN221.ClinicDental.Data.Common.Interface
{
    public interface IClinicServicesRepository : IRepositoryBase<ClinicService>
    {
        Task<ClinicService?> GetClinicServiceByIdAndClinicIdAsync(int serviceId, int clinicId);
        Task<bool> IsServiceUsedByAnyClinicAsync(int serviceId);

    }
}
