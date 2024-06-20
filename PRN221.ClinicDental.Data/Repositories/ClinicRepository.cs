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
    public class ClinicRepository : RepositoryBase<Clinic>, IClinicRepository
    {
        public ClinicRepository(ClinicDentalDbContext context) : base(context)
        {
        }

        public Task<List<Clinic>> GetAllClinics()
        {
            return _context.Clinics.ToListAsync();
        }

        public async Task<List<Clinic>> GetClinicsByOnServiceId(int serviceId)
        {
            return await _context.ClinicServices.Include(x=>x.Clinic).Where(x=>x.ServiceId == serviceId).Select(x=>x.Clinic).ToListAsync();
        }
    }
}
