using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using PRN221.ClinicDental.Business.DTO.Request.Appointment;
using PRN221.ClinicDental.Business.Helper;
using PRN221.ClinicDental.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Security.Claims;
using System.Security.Claims;

namespace PRN221.ClinicDental.Business.Validate
{
    public class SlotAvailabilityAttribute : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            var model = (AppointmentRequestModel)validationContext.ObjectInstance;

            var service = validationContext.GetService<IAppointmentService>();
            

           
          

            // Check if the slot is valid
            if (!TimeSlotHelper.IsValidSlot(model.Slot, model.AppointmentDate))
            {
                return new ValidationResult("Slot time must be in the future.");
            }

            // Check the number of appointments in the same slot
            int appointmentCount = service.GetAppointmentsCountForSlot(model.ClinicId, model.DentistId, model.AppointmentDate, model.Slot);
            if (appointmentCount >= 3)
            {
                return new ValidationResult("This slot is fully booked. Please choose another slot.");
            }

            
           

            return ValidationResult.Success;
        }
    }
}
