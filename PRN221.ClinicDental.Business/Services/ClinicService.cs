using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using PRN221.ClinicDental.Data.Models;
using PRN221.ClinicDental.Services.Interfaces;

namespace PRN221.ClinicDental.Services
{
    public class ClinicService : IClinicService
    {
        private readonly ClinicDentalContext _context;

        public ClinicService(ClinicDentalContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Clinic>> GetClinicsAsync()
        {
            return await _context.Clinics
                .Include(c => c.ClinicOwner)
                .Include(c => c.DoctorDetails)
                .ToListAsync();
        }

        public async Task<Clinic> GetClinicByIdAsync(int clinicId)
        {
            return await _context.Clinics
                .Include(c => c.ClinicOwner)
                .Include(c => c.DoctorDetails)
                .FirstOrDefaultAsync(c => c.ClinicId == clinicId);
        }

        public async Task<IEnumerable<Clinic>> GetClinicsByOwnerIdAsync(int ownerId)
        {
            return await _context.Clinics
                .Include(c => c.ClinicOwner)
                .Include(c => c.DoctorDetails)
                .Where(c => c.ClinicOwnerId == ownerId)
                .ToListAsync();
        }

        public async Task AddClinicAsync(Clinic clinic)
        {
            _context.Clinics.Add(clinic);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateClinicAsync(Clinic clinic)
        {
            _context.Entry(clinic).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }

        public async Task DeleteClinicAsync(int clinicId)
        {
            var clinic = await _context.Clinics.FindAsync(clinicId);
            if (clinic != null)
            {
                _context.Clinics.Remove(clinic);
                await _context.SaveChangesAsync();
            }
        }
    }
}
