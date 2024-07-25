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

        public async Task<List<DentistDetail>> GetDentistsByServiceAndClinic(int serviceId, int clinicId)
        {
            return await _context.DentistServices
                .Include(ds => ds.Dentist)
                .ThenInclude(dd => dd.User)
                .Where(ds => ds.ServiceId == serviceId && ds.Dentist.ClinicId == clinicId)
                .Select(ds => ds.Dentist)
                .ToListAsync();
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
        public async Task<List<ClinicService>> GetServicesByClinicId(int clinicId)
        {
            return await _context.ClinicServices
                .Include(cs => cs.Service)
                .Where(cs => cs.ClinicId == clinicId)
                .ToListAsync();
        }

        public async Task<bool> ServiceNameExistsAsync(string serviceName)
        {
            return await _context.Services.AnyAsync(s => s.ServiceName == serviceName);
        }

        public async Task<ClinicService> GetServicesByClinicServiceId(int clinicServiceId)
        {
            return await _context.ClinicServices
               .Include(cs => cs.Service)
               .FirstOrDefaultAsync(x => x.ClinicServiceId == clinicServiceId);
        }

        public async Task<List<Service>> GetServiceByClinicId(int clinicId)
        {
            var clinicServices= await _context.ClinicServices.Include(x=>x.Service).Where(x=>x.ClinicId==clinicId).ToListAsync();
            return  clinicServices.Select(x => x.Service).ToList();
        }
    }
}
