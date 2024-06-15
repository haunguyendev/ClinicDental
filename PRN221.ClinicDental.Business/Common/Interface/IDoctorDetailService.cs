using System.Collections.Generic;
using System.Threading.Tasks;
using PRN221.ClinicDental.Data.Models;

namespace PRN221.ClinicDental.Services.Interfaces
{
    public interface IDoctorDetailService
    {
        Task<IEnumerable<DoctorDetail>> GetDoctorDetailsAsync();
        Task<DoctorDetail> GetDoctorDetailByIdAsync(int doctorDetailsId);
        Task<IEnumerable<DoctorDetail>> GetDoctorDetailsByClinicIdAsync(int clinicId);
        Task<IEnumerable<DoctorDetail>> GetDoctorDetailsByDoctorIdAsync(int doctorId);
        Task AddDoctorDetailAsync(DoctorDetail doctorDetail);
        Task UpdateDoctorDetailAsync(DoctorDetail doctorDetail);
        Task DeleteDoctorDetailAsync(int doctorDetailsId);
    }
}
