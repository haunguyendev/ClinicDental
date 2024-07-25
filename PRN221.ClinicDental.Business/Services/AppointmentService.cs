using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using PRN221.ClinicDental.Business.Common.Enum;
using PRN221.ClinicDental.Business.DTO.Request.Appointment;
using PRN221.ClinicDental.Business.DTO.Response.Appointment;
using PRN221.ClinicDental.Business.Helper;
using PRN221.ClinicDental.Data.Common.Interface;
using PRN221.ClinicDental.Data.Models;
using PRN221.ClinicDental.Data.Repositories;
using PRN221.ClinicDental.Services.Interfaces;

namespace PRN221.ClinicDental.Services
{
    public class AppointmentService : IAppointmentService
    {
        private readonly IUnitOfWork _unitOfWork;

        public AppointmentService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task CreateAppointmentAsync(AppointmentRequestModel? model, int customerId)
        {
           
            var appointment = new Appointment
            {
                CustomerId = customerId,
                ServiceId = model.ServiceId,
                ClinicId = model.ClinicId,
                DentistId = model.DentistId,
                AppointmentTime = model.AppointmentDate,
                Slot=model.Slot,
                PhoneNumber = model.PhoneNumber,
                Notes = model.Notes,
                Status = "Scheduled",
                
            };

            await _unitOfWork.AppointmentRepository.CreateAsync(appointment);
            await _unitOfWork.CommitAsync();

        }

        public bool CustomerHasAppointment(int customerId, DateTime appointmentDate, int slot)
        {
            return _unitOfWork.AppointmentRepository.CustomerHasAppointment(customerId, appointmentDate, slot);
        }

        public int GetAppointmentsCountForSlot(int clinicId, int dentistId, DateTime appointmentDate, int slot)
        {
           return _unitOfWork.AppointmentRepository.GetAppointmentsCountForSlot(clinicId, dentistId, appointmentDate, slot);
        }

        public async Task<List<AppointmentResponseModel>> GetPastAppointmentsAsync(int customerId)
        {
            var appointments = await _unitOfWork.AppointmentRepository.GetListAppointmentByCustomerIdAsync(customerId);
            var pastAppointments = new List<AppointmentResponseModel>();

            
            pastAppointments = appointments
                .Where(appointment =>
                    appointment.Status !=AppointmentStatusTypeEnum.Scheduled || !TimeSlotHelper.IsValidSlot(appointment.Slot, appointment.AppointmentTime))
                .OrderByDescending(appointment => appointment.AppointmentTime) 
                .ThenByDescending(appointment => appointment.Slot) 
                .Select(appointment => MapToResponseModel(appointment))
                .ToList();


            return pastAppointments;
        }

        public async Task<List<AppointmentResponseModel>> GetUpcomingAppointmentsAsync(int customerId)
        {
            var appointments = await _unitOfWork.AppointmentRepository.GetListAppointmentByCustomerIdAsync(customerId);
            var upcomingAppointments = new List<AppointmentResponseModel>();

            foreach (var appointment in appointments)
            {
               
                if (appointment.Status == AppointmentStatusTypeEnum.Scheduled && TimeSlotHelper.IsValidSlot(appointment.Slot, appointment.AppointmentTime))
                {
                    upcomingAppointments.Add(MapToResponseModel(appointment));
                }
            }

            
            upcomingAppointments.Sort((a, b) =>
            {
                
                var dateComparison = a.AppointmentDate.CompareTo(b.AppointmentDate);
                if (dateComparison != 0)
                {
                    return dateComparison;
                }

               
                return a.TimeRange.CompareTo(b.TimeRange);
            });

            return upcomingAppointments;
        }
        private AppointmentResponseModel MapToResponseModel(Appointment appointment)
        {
            return new AppointmentResponseModel
            {
                AppointmentId = appointment.AppointmentId,
                DentistName = appointment.Dentist.Name,
                AppointmentDate = appointment.AppointmentTime.Date,
                TimeRange = TimeSlotHelper.GetTimeRange((int)appointment.Slot), // Get the time range for the slot
                ClinicName = appointment.Clinic.Name,
                Status = appointment.Status,
                ServiceName = appointment.Service.ServiceName,
                Address=appointment.Clinic.Address.StreetAddress+", "+ appointment.Clinic.Address.District,
                PhoneNumber = appointment.PhoneNumber,
                Notes = appointment.Notes
            };
        }
        private AppointmentDentistResponseModel MapToAppointmentDentistResponseModel(Appointment appointment)
        {
            return new AppointmentDentistResponseModel
            {
                AppointmentId = appointment.AppointmentId,
                CustomerName = appointment.Customer.Name,
                AppointmentDate = appointment.AppointmentTime.Date,
                TimeRange = TimeSlotHelper.GetTimeRange((int)appointment.Slot), // Get the time range for the slot
                ClinicName = appointment.Clinic.Name,
                Status = appointment.Status,
                ServiceName = appointment.Service.ServiceName,
                Address = appointment.Clinic.Address.StreetAddress + ", " + appointment.Clinic.Address.District,
                PhoneNumber = appointment.PhoneNumber,
                Notes = appointment.Notes
            };
        }
        public async Task<bool> RescheduleAppointmentAsync(int appointmentId, DateTime newDate, int newSlot)
        {
            if (!TimeSlotHelper.IsValidSlot(newSlot, newDate))
            {
                throw new ArgumentException("The new appointment time must be in the future and the slot must be valid.");
            }

            var appointment = await _unitOfWork.AppointmentRepository.GetById(appointmentId);
            if (appointment == null)
            {
                throw new KeyNotFoundException("Appointment not found.");
            }

            appointment.AppointmentTime = newDate;
            appointment.Slot = newSlot;
            try
            {
                await _unitOfWork.AppointmentRepository.UpdateAsync(appointment);
                await _unitOfWork.CommitAsync();
            } catch (KeyNotFoundException ex)
            {
                return false;
            }
             return true;
        }

        public async Task<int?> GetClinicIdByAppointmentId(int id)
        {
           var result = await _unitOfWork.AppointmentRepository.GetById(id);
            return result.ClinicId;
        }

        public async Task<int?> GetDentistIdByAppoimentId(int id)
        {
            var result = await _unitOfWork.AppointmentRepository.GetById(id);
            return result.DentistId;
        }

        public async Task<bool> CancelAppointmentAsync(int appointmentId)
        {
            return await _unitOfWork.AppointmentRepository.CancelAppointmentAsync(appointmentId, AppointmentStatusTypeEnum.Canceled);

        }

        public async Task<AppointmentResponseModel> GetAppointmentByIdAsync(int appointmentId)
        {
            var appointment = await _unitOfWork.AppointmentRepository.GetById(appointmentId);

            if (appointment == null)
            {
                return null;
            }

            return MapToResponseModel(appointment);
        }

        public async Task<List<AppointmentDentistResponseModel>> GetUpcomingAppointmentsByDentistAsync(int dentistId)
        {
            var appointments = await _unitOfWork.AppointmentRepository.GetListAppointmentByDentistIdAsync(dentistId);
            var upcomingAppointments = new List<AppointmentDentistResponseModel>();

            foreach (var appointment in appointments)
            {

                if (appointment.Status == AppointmentStatusTypeEnum.Scheduled && TimeSlotHelper.IsValidSlot(appointment.Slot, appointment.AppointmentTime))
                {
                    upcomingAppointments.Add(MapToAppointmentDentistResponseModel(appointment));
                }
            }


            upcomingAppointments.Sort((a, b) =>
            {

                var dateComparison = a.AppointmentDate.CompareTo(b.AppointmentDate);
                if (dateComparison != 0)
                {
                    return dateComparison;
                }


                return a.TimeRange.CompareTo(b.TimeRange);
            });

            return upcomingAppointments;
        }

        public async Task<List<AppointmentDentistResponseModel>> GetPastAppointmentsByDentistAsync(int dentistId)
        {

            var appointments = await _unitOfWork.AppointmentRepository.GetListAppointmentByDentistIdAsync(dentistId);
            var pastAppointments = new List<AppointmentDentistResponseModel>();


            pastAppointments = appointments
                .Where(appointment =>
                    appointment.Status != AppointmentStatusTypeEnum.Scheduled || !TimeSlotHelper.IsValidSlot(appointment.Slot, appointment.AppointmentTime))
                .OrderByDescending(appointment => appointment.AppointmentTime)
                .ThenByDescending(appointment => appointment.Slot)
                .Select(appointment => MapToAppointmentDentistResponseModel(appointment))
                .ToList();


            return pastAppointments;
        }

        public async Task<bool> UpdateAppointmentStatusAsync(int appointmentId, string status)
        {
            var appointment = await _unitOfWork.AppointmentRepository.GetById(appointmentId);
            if (appointment == null)
            {
                return false;
            }

            appointment.Status = status;
            await _unitOfWork.AppointmentRepository.UpdateAsync(appointment);
            await _unitOfWork.CommitAsync(); 
            return true;
        }

        public async Task<int> GetTotalCompletedAppointmentsThisWeekAsync()
        {
            var startOfWeek = DateTime.UtcNow.AddDays(-(int)DateTime.UtcNow.DayOfWeek);
            var endOfWeek = startOfWeek.AddDays(7);

            var query = await _unitOfWork.AppointmentRepository.GetAllAppointmentsAsync();
            return query.Count(a => a.Status == AppointmentStatusTypeEnum.Completed && a.AppointmentTime >= startOfWeek && a.AppointmentTime < endOfWeek);
        }

        public async Task<decimal> GetMonthlyRevenueAsync()
        {
            var startOfMonth = new DateTime(DateTime.UtcNow.Year, DateTime.UtcNow.Month, 1);
            var endOfMonth = startOfMonth.AddMonths(1);

            var appointments = await _unitOfWork.AppointmentRepository.GetAllAppointmentsAsync();
            var completedAppointments = appointments
                .Where(a => a.Status == AppointmentStatusTypeEnum.Completed && a.AppointmentTime >= startOfMonth && a.AppointmentTime < endOfMonth);

            decimal totalRevenue = 0;
            foreach (var appointment in completedAppointments)
            {
                var clinicService = await _unitOfWork.ClinicServicesRepository.GetClinicServiceByIdAndClinicIdAsync(appointment.ServiceId,appointment.ClinicId);
                if (clinicService != null)
                {
                    totalRevenue += clinicService.Price;
                }
            }

            return totalRevenue;
        }
    }
}
