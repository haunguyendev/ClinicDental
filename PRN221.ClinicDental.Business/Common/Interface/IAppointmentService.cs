using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using PRN221.ClinicDental.Business.DTO.Request.Appointment;
using PRN221.ClinicDental.Business.DTO.Response.Appointment;
using PRN221.ClinicDental.Data.Models;

namespace PRN221.ClinicDental.Services.Interfaces
{
    public interface IAppointmentService
    {
        Task CreateAppointmentAsync(AppointmentRequestModel? model, int customerId);
        int GetAppointmentsCountForSlot(int clinicId, int dentistId, DateTime appointmentDate, int slot);
        bool CustomerHasAppointment(int customerId, int clinicId, DateTime appointmentDate, int slot);
        Task<int?> GetClinicIdByAppointmentId(int id);
        Task<int?> GetDentistIdByAppoimentId(int id);
        Task<AppointmentResponseModel> GetAppointmentByIdAsync(int appointmentId);

        Task<List<AppointmentResponseModel>> GetUpcomingAppointmentsAsync(int customerId);
        Task<List<AppointmentResponseModel>> GetPastAppointmentsAsync(int customerId);
        Task<bool> RescheduleAppointmentAsync(int appointmentId, DateTime newDate, int newSlot);
        Task<bool> CancelAppointmentAsync(int appointmentId);
    }
}
