using System.Collections.Generic;
using System.Threading.Tasks;
using PRN221.ClinicDental.Data.Models;

namespace PRN221.ClinicDental.Services.Interfaces
{
    public interface IPatientService
    {
        Task<IEnumerable<Patient>> GetPatientsAsync();
        Task<Patient> GetPatientByIdAsync(int patientId);
        Task<IEnumerable<Patient>> GetPatientsByUserIdAsync(int userId);
        Task AddPatientAsync(Patient patient);
        Task UpdatePatientAsync(Patient patient);
        Task DeletePatientAsync(int patientId);
    }
}
