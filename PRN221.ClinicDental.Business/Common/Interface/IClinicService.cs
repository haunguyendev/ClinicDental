using System.Collections.Generic;
using System.Threading.Tasks;
using PRN221.ClinicDental.Data.Models;

namespace PRN221.ClinicDental.Services.Interfaces
{
    public interface IClinicService
    {
        Task<IEnumerable<Clinic>> GetClinicsAsync();
        Task<Clinic> GetClinicByIdAsync(int clinicId);
        Task<IEnumerable<Clinic>> GetClinicsByOwnerIdAsync(int ownerId);
        Task AddClinicAsync(Clinic clinic);
        Task UpdateClinicAsync(Clinic clinic);
        Task DeleteClinicAsync(int clinicId);
    }
}
