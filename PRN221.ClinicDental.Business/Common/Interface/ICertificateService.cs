using System.Collections.Generic;
using System.Threading.Tasks;
using PRN221.ClinicDental.Data.Models;

namespace PRN221.ClinicDental.Services.Interfaces
{
    public interface ICertificateService
    {
        Task<IEnumerable<Certificate>> GetCertificatesAsync();
        Task<Certificate> GetCertificateByIdAsync(int certificateId);
        Task<IEnumerable<Certificate>> GetCertificatesByDoctorDetailsIdAsync(int doctorDetailsId);
        Task AddCertificateAsync(Certificate certificate);
        Task UpdateCertificateAsync(Certificate certificate);
        Task DeleteCertificateAsync(int certificateId);
    }
}
