using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using PRN221.ClinicDental.Business.Common.Interface;
using PRN221.ClinicDental.Business.DTO.Request.Appointment;
using PRN221.ClinicDental.Business.DTO.Response.Clinic;
using PRN221.ClinicDental.Business.DTO.Response.Dentist;
using PRN221.ClinicDental.Business.DTO.Response.ServiceResponse;
using PRN221.ClinicDental.Data.Models;
using PRN221.ClinicDental.Services;
using PRN221.ClinicDental.Services.Interfaces;
using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;

namespace PRN221.ClinicDental.Presentation.Pages.Customer
{
    [Authorize(Roles = "Customer")]
    public class AppointmentModel : PageModel
    {
        private readonly IServiceService _serviceService;
        private readonly IAppointmentService _appointmentService;




        public AppointmentModel(IServiceService serviceService,IAppointmentService appointmentService)
        {
            _serviceService = serviceService;
            _appointmentService = appointmentService;
        }
        [TempData]
        public string ErrorMessage { get; set; }

        [BindProperty]
        public AppointmentRequestModel AppointmentRequest { get; set; }


        public List<DentistResponseModel> Dentists { get; set; }


        public async Task<IActionResult> OnGetAsync(int serviceId, int clinicId)
        {
            var service = await _serviceService.GetServiceById(serviceId);

            if (service == null)
            {
                return NotFound();
            }
            AppointmentRequest = new AppointmentRequestModel
            {
                ServiceId = serviceId,
                ClinicId = clinicId
            };

            Dentists = await _serviceService.GetDentistsByServiceAndClinic(serviceId, clinicId);
            return Page();
        }
        public async Task<IActionResult> OnPostAsync()
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return Page();
                }

                var userId = User.FindFirstValue("UserId");
                if (!int.TryParse(userId, out var customerId))
                {
                    ModelState.AddModelError(string.Empty, "Invalid user ID.");
                    return Page();
                }

                if (AppointmentRequest == null)
                {
                    ModelState.AddModelError(string.Empty, "Appointment request is null.");
                    return Page();
                }
                
                if ( _appointmentService.CustomerHasAppointment(customerId, AppointmentRequest.AppointmentDate, AppointmentRequest.Slot))

                {

                    ErrorMessage = "You already have an appointment in this slot on the same date.";
                    return RedirectToPage("/Customer/Appointment", new { clinicId = AppointmentRequest.ClinicId, serviceId = AppointmentRequest.ServiceId });
                }

                await _appointmentService.CreateAppointmentAsync(AppointmentRequest, customerId);
                TempData["Message"] = "Appointment successfully created.";

                return RedirectToPage("/Index");
            }
            catch (Exception ex)
            {
                ModelState.AddModelError(string.Empty, $"Error creating appointment: {ex.Message}");
                return Page();
            }
        }
        public async Task<IActionResult> OnGetLogout()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToPage("/Accounts/Login");
        }



    }
}
