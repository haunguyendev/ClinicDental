using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using PRN221.ClinicDental.Business.DTO.Request.Appointment;
using PRN221.ClinicDental.Business.DTO.Response.Appointment;
using PRN221.ClinicDental.Business.Helper;
using PRN221.ClinicDental.Data.Models;
using PRN221.ClinicDental.Services.Interfaces;
using System.ComponentModel.DataAnnotations;
using System.Security.Claims;

namespace PRN221.ClinicDental.Presentation.Pages.Customer
{
    public class ViewAppointmentModel : PageModel
    {
        private readonly IAppointmentService _appointmentService;
        public ViewAppointmentModel(IAppointmentService appointmentService)
        {
            _appointmentService = appointmentService;
            
        }

        public List<AppointmentResponseModel> UpcomingAppointments { get; set; }
        public List<AppointmentResponseModel> PastAppointments { get; set; }
        [BindProperty]
        public RescheduleAppointmentRequestModel RescheduleModel { get; set; }
        public async Task OnGetAsync()
        {
            var customerId = int.Parse(User.FindFirstValue("UserId"));
            UpcomingAppointments = await _appointmentService.GetUpcomingAppointmentsAsync(customerId);
            PastAppointments = await _appointmentService.GetPastAppointmentsAsync(customerId);
        }
        public async Task<IActionResult> OnPostRescheduleAsync()
        {
            var customerId = int.Parse(User.FindFirstValue("UserId"));

            try
            {
                if (!ModelState.IsValid)
                {
                    TempData["ErrorMessage"] = "Invalid input data. Please check your input and try again.";
                    return RedirectToPage("./ViewAppointment");
                }

                if (!TimeSlotHelper.IsValidSlot(RescheduleModel.NewSlot, RescheduleModel.NewDate))
                {
                    TempData["ErrorMessage"] = "Slot time must be in the future.";
                    return RedirectToPage("./ViewAppointment");
                }

                var clinicIdExisted = (int) await _appointmentService.GetClinicIdByAppointmentId(RescheduleModel.AppointmentId);
                if (clinicIdExisted == null)
                {
                    TempData["ErrorMessage"] = "Clinic ID of appointment not found!";
                    return RedirectToPage("./ViewAppointment");
                }

                var dentistIdExisted =(int) await _appointmentService.GetDentistIdByAppoimentId(RescheduleModel.AppointmentId);
                if (dentistIdExisted == null)
                {
                    TempData["ErrorMessage"] = "Dentist of appointment not found!";
                    return RedirectToPage("./ViewAppointment");
                }

                int appointmentCount =   _appointmentService.GetAppointmentsCountForSlot(clinicIdExisted, dentistIdExisted, RescheduleModel.NewDate, RescheduleModel.NewSlot);

                if (_appointmentService.CustomerHasAppointment(customerId, clinicIdExisted, RescheduleModel.NewDate, RescheduleModel.NewSlot))
                {
                    TempData["ErrorMessage"] = "You already have an appointment in this slot on the same date.";
                    return RedirectToPage("./ViewAppointment");
                }

                bool success =  await _appointmentService.RescheduleAppointmentAsync(RescheduleModel.AppointmentId, RescheduleModel.NewDate, RescheduleModel.NewSlot);

                if (success)
                {
                    TempData["SuccessMessage"] = "Appointment successfully rescheduled!";
                }
                else
                {
                    TempData["ErrorMessage"] = "Failed to reschedule the appointment. Please try again.";
                }

                return RedirectToPage("./ViewAppointment");
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = $"Error rescheduling appointment: {ex.Message}";
                return RedirectToPage("./ViewAppointment");
            }
           
        }
        public async Task<IActionResult> OnPostCancelAsync(int appointmentId)
        {
            var result = await _appointmentService.CancelAppointmentAsync(appointmentId);
            if (result)
            {
                TempData["SuccessMessage"] = "Appointment canceled successfully.";
            }
            else
            {
                TempData["ErrorMessage"] = "Failed to cancel appointment. Please try again.";
            }
            return RedirectToPage();
        }


    }
}
