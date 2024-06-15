using System.Collections.Generic;
using System.Threading.Tasks;
using PRN221.ClinicDental.Data.Models;

namespace PRN221.ClinicDental.Services.Interfaces
{
    public interface IDoctorServiceService
    {
        Task<IEnumerable<DoctorService>> GetDoctorServicesAsync();
        Task<DoctorService> GetDoctorServiceByIdAsync(int doctorServiceId);
        Task<IEnumerable<DoctorService>> GetDoctorServicesByDoctorIdAsync(int doctorId);
        Task<IEnumerable<DoctorService>> GetDoctorServicesByServiceIdAsync(int serviceId);
        Task AddDoctorServiceAsync(DoctorService doctorService);
        Task UpdateDoctorServiceAsync(DoctorService doctorService);
        Task DeleteDoctorServiceAsync(int doctorServiceId);
    }
}
