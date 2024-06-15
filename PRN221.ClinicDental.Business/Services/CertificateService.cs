using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using PRN221.ClinicDental.Data.Models;
using PRN221.ClinicDental.Services.Interfaces;

namespace PRN221.ClinicDental.Services
{
    public class CertificateService : ICertificateService
    {
        private readonly ClinicDentalContext _context;

        public CertificateService(ClinicDentalContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Certificate>> GetCertificatesAsync()
        {
            return await _context.Certificates
                .Include(c => c.DoctorDetails)
                .ToListAsync();
        }

        public async Task<Certificate> GetCertificateByIdAsync(int certificateId)
        {
            return await _context.Certificates
                .Include(c => c.DoctorDetails)
                .FirstOrDefaultAsync(c => c.CertificateId == certificateId);
        }

        public async Task<IEnumerable<Certificate>> GetCertificatesByDoctorDetailsIdAsync(int doctorDetailsId)
        {
            return await _context.Certificates
                .Include(c => c.DoctorDetails)
                .Where(c => c.DoctorDetailsId == doctorDetailsId)
                .ToListAsync();
        }

        public async Task AddCertificateAsync(Certificate certificate)
        {
            _context.Certificates.Add(certificate);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateCertificateAsync(Certificate certificate)
        {
            _context.Entry(certificate).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }

        public async Task DeleteCertificateAsync(int certificateId)
        {
            var certificate = await _context.Certificates.FindAsync(certificateId);
            if (certificate != null)
            {
                _context.Certificates.Remove(certificate);
                await _context.SaveChangesAsync();
            }
        }
    }
}
