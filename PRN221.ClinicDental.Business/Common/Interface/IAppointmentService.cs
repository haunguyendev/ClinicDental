using System.Collections.Generic;
using System.Threading.Tasks;
using PRN221.ClinicDental.Business.DTO.Request.Appointment;
using PRN221.ClinicDental.Data.Models;

namespace PRN221.ClinicDental.Services.Interfaces
{
    public interface IAppointmentService
    {
        Task CreateAppointmentAsync(AppointmentRequestModel? model, int customerId);
    }
}
