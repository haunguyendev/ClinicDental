using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using PRN221.ClinicDental.Data.Models;
using PRN221.ClinicDental.Services.Interfaces;

namespace PRN221.ClinicDental.Services
{
    public class DoctorServiceService : IDoctorServiceService
    {
        private readonly ClinicDentalContext _context;

        public DoctorServiceService(ClinicDentalContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<DoctorService>> GetDoctorServicesAsync()
        {
            return await _context.DoctorServices
                .Include(ds => ds.Doctor)
                .Include(ds => ds.Service)
                .ToListAsync();
        }

        public async Task<DoctorService> GetDoctorServiceByIdAsync(int doctorServiceId)
        {
            return await _context.DoctorServices
                .Include(ds => ds.Doctor)
                .Include(ds => ds.Service)
                .FirstOrDefaultAsync(ds => ds.DoctorServiceId == doctorServiceId);
        }

        public async Task<IEnumerable<DoctorService>> GetDoctorServicesByDoctorIdAsync(int doctorId)
        {
            return await _context.DoctorServices
                .Include(ds => ds.Doctor)
                .Include(ds => ds.Service)
                .Where(ds => ds.DoctorId == doctorId)
                .ToListAsync();
        }

        public async Task<IEnumerable<DoctorService>> GetDoctorServicesByServiceIdAsync(int serviceId)
        {
            return await _context.DoctorServices
                .Include(ds => ds.Doctor)
                .Include(ds => ds.Service)
                .Where(ds => ds.ServiceId == serviceId)
                .ToListAsync();
        }

        public async Task AddDoctorServiceAsync(DoctorService doctorService)
        {
            _context.DoctorServices.Add(doctorService);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateDoctorServiceAsync(DoctorService doctorService)
        {
            _context.Entry(doctorService).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }

        public async Task DeleteDoctorServiceAsync(int doctorServiceId)
        {
            var doctorService = await _context.DoctorServices.FindAsync(doctorServiceId);
            if (doctorService != null)
            {
                _context.DoctorServices.Remove(doctorService);
                await _context.SaveChangesAsync();
            }
        }
    }
}
