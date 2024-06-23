using System.Collections.Generic;
using System.Threading.Tasks;
using PRN221.ClinicDental.Business.DTO.Response.Dentist;
using PRN221.ClinicDental.Data.Models;

namespace PRN221.ClinicDental.Services.Interfaces
{
    public interface IDentistDetailService
    {
        Task<List<DentistResponseModel>> GetDentistsByClinicId(int clinicId);
    }
}
