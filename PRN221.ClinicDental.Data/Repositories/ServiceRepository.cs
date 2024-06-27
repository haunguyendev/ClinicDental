using Microsoft.EntityFrameworkCore;
using Microsoft.Identity.Client;
using PRN221.ClinicDental.Data.Common.Interface;
using PRN221.ClinicDental.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PRN221.ClinicDental.Data.Repositories
{
    public class ServiceRepository : RepositoryBase<Service>, IServiceRepository
    {
        public ServiceRepository(ClinicDentalDbContext context) : base(context)
        {
            
        }

        public Task<List<Service>> GetAllServices()
        {
            return _context.Services.ToListAsync();
        }
        public async Task<Service> GetServiceById(int id)
        {
            return await _context.Services.Include(x => x.ClinicServices)
                .ThenInclude(x => x.Clinic).FirstOrDefaultAsync(x => x.ServiceId == id);
                
        }

        public async Task<List<Service>> GetServiceByListId(List<int> ids)
        {
            return await _context.Services.Include(x => x.ClinicServices)
                .ThenInclude(x => x.Clinic).AsNoTracking().Where(x => ids.Contains(x.ServiceId)).ToListAsync();
        }
    }
}
