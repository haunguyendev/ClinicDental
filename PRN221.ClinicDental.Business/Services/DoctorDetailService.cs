using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using PRN221.ClinicDental.Data.Models;
using PRN221.ClinicDental.Services.Interfaces;

namespace PRN221.ClinicDental.Services
{
    public class DoctorDetailService : IDoctorDetailService
    {
        private readonly ClinicDentalContext _context;

        public DoctorDetailService(ClinicDentalContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<DoctorDetail>> GetDoctorDetailsAsync()
        {
            return await _context.DoctorDetails
                .Include(dd => dd.Doctor)
                .Include(dd => dd.Clinic)
                .Include(dd => dd.Certificates)
                .Include(dd => dd.DoctorServices)
                .ToListAsync();
        }

        public async Task<DoctorDetail> GetDoctorDetailByIdAsync(int doctorDetailsId)
        {
            return await _context.DoctorDetails
                .Include(dd => dd.Doctor)
                .Include(dd => dd.Clinic)
                .Include(dd => dd.Certificates)
                .Include(dd => dd.DoctorServices)
                .FirstOrDefaultAsync(dd => dd.DoctorDetailsId == doctorDetailsId);
        }

        public async Task<IEnumerable<DoctorDetail>> GetDoctorDetailsByClinicIdAsync(int clinicId)
        {
            return await _context.DoctorDetails
                .Include(dd => dd.Doctor)
                .Include(dd => dd.Clinic)
                .Include(dd => dd.Certificates)
                .Include(dd => dd.DoctorServices)
                .Where(dd => dd.ClinicId == clinicId)
                .ToListAsync();
        }

        public async Task<IEnumerable<DoctorDetail>> GetDoctorDetailsByDoctorIdAsync(int doctorId)
        {
            return await _context.DoctorDetails
                .Include(dd => dd.Doctor)
                .Include(dd => dd.Clinic)
                .Include(dd => dd.Certificates)
                .Include(dd => dd.DoctorServices)
                .Where(dd => dd.DoctorId == doctorId)
                .ToListAsync();
        }

        public async Task AddDoctorDetailAsync(DoctorDetail doctorDetail)
        {
            _context.DoctorDetails.Add(doctorDetail);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateDoctorDetailAsync(DoctorDetail doctorDetail)
        {
            _context.Entry(doctorDetail).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }

        public async Task DeleteDoctorDetailAsync(int doctorDetailsId)
        {
            var doctorDetail = await _context.DoctorDetails.FindAsync(doctorDetailsId);
            if (doctorDetail != null)
            {
                _context.DoctorDetails.Remove(doctorDetail);
                await _context.SaveChangesAsync();
            }
        }
    }
}
