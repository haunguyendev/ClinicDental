using Microsoft.EntityFrameworkCore;
using PRN221.ClinicDental.Data.Common.Interface;
using PRN221.ClinicDental.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace PRN221.ClinicDental.Data.Repositories
{
    public class ClinicRepository : RepositoryBase<Clinic>, IClinicRepository
    {
        public ClinicRepository(ClinicDentalDbContext context) : base(context)
        {
        }

        public async Task<bool> AddNewClinic(Clinic clinic, List<Service> service)
        {
            var serviceValues = service.Select(x => x.ServiceId);
            var selectedService = _context.ClinicServices.Where(x=> serviceValues.Contains(x.ServiceId)).ToList();

            clinic.ClinicServices = selectedService;
            _context.Clinics.Add(clinic);
           await _context.SaveChangesAsync();
            return true;
        }

        public Task<List<Clinic>> GetAllClinics()
        {
            return _context.Clinics.Include(x=>x.Address).Include(x=> x.ClinicServices).ToListAsync();
        }

        public async Task<List<Clinic>> GetClinicsByOnServiceId(int serviceId)
        {
            return await _context.ClinicServices.Include(x=>x.Clinic).Where(x=>x.ServiceId == serviceId).Select(x=>x.Clinic).ToListAsync();
        }

        public async Task<List<Clinic>> SearchClinics(Expression<Func<Clinic, bool>> predicate)
        {
            return await _context.Clinics.Include(x=>x.Address).Where(predicate).ToListAsync();
        }
    }
}
