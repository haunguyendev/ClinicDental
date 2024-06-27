using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using PRN221.ClinicDental.Business.DTO.Request.Appointment;
using PRN221.ClinicDental.Data.Common.Interface;
using PRN221.ClinicDental.Data.Models;
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
    }
}
