using Microsoft.EntityFrameworkCore;
using PRN221.ClinicDental.Data.Common.Interface;
using PRN221.ClinicDental.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PRN221.ClinicDental.Data.Repositories
{
    public class ClinicServicesRepository : RepositoryBase<ClinicService>, IClinicServicesRepository
    {
        public ClinicServicesRepository(ClinicDentalDbContext context) : base(context)
        {
        }

        public async Task<ClinicService?> GetClinicServiceByIdAndClinicIdAsync(int serviceId, int clinicId)
        {
            return await _context.ClinicServices.SingleOrDefaultAsync(x => x.ServiceId == serviceId && x.ClinicId == clinicId);
        }

        public async Task<bool> IsServiceUsedByAnyClinicAsync(int serviceId)
        {
            return await _context.ClinicServices.AnyAsync(cs => cs.ServiceId == serviceId);
        }
    }
}
