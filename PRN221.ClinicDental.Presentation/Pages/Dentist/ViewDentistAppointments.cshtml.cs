using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using PRN221.ClinicDental.Business.DTO.Response.Appointment;
using PRN221.ClinicDental.Services.Interfaces;
using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;

namespace PRN221.ClinicDental.Presentation.Pages.Dentist
{
    [Authorize(Roles = "Dentist")]
    public class ViewAppointmentsModel : PageModel
    {
        private readonly IAppointmentService _appointmentService;

        public List<AppointmentDentistResponseModel> UpcomingAppointments { get; set; }
        public List<AppointmentDentistResponseModel> PastAppointments { get; set; }

        public ViewAppointmentsModel(IAppointmentService appointmentService)
        {
            _appointmentService = appointmentService;
        }

        public async Task OnGetAsync()
        {
            var dentistId = int.Parse(User.FindFirstValue("UserId"));
            UpcomingAppointments = await _appointmentService.GetUpcomingAppointmentsByDentistAsync(dentistId);
            PastAppointments = await _appointmentService.GetPastAppointmentsByDentistAsync(dentistId);
        }

        public async Task<IActionResult> OnPostCompleteAsync(int appointmentId)
        {
            await _appointmentService.UpdateAppointmentStatusAsync(appointmentId, "Completed");
            return RedirectToPage();
        }

        public async Task<IActionResult> OnPostMissedAsync(int appointmentId)
        {
            await _appointmentService.UpdateAppointmentStatusAsync(appointmentId, "Missed");
            return RedirectToPage();
        }
        public async Task<IActionResult> OnGetLogout()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToPage("/Accounts/Login");
        }
    }
}
