using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using PRN221.ClinicDental.Business.Common.Interface;
using PRN221.ClinicDental.Business.DTO.Request.Appointment;
using PRN221.ClinicDental.Business.DTO.Response.Clinic;
using PRN221.ClinicDental.Business.DTO.Response.Dentist;
using PRN221.ClinicDental.Business.DTO.Response.ServiceResponse;
using PRN221.ClinicDental.Services;
using PRN221.ClinicDental.Services.Interfaces;
using System.Security.Claims;

namespace PRN221.ClinicDental.Presentation.Pages.Customer
{
   
        public class AppointmentModel : PageModel
        {
            private readonly IServiceService _serviceService;
            private readonly IClinicService _clinicService;
            private readonly IDentistDetailService _dentistDetailService;
            private readonly IAppointmentService _appointmentService;
        public List<DentistResponseModel> Dentists { get; set; }

        public AppointmentModel(IServiceService serviceService, IClinicService clinicService,IDentistDetailService dentistDetailService,IAppointmentService appointmentService)
            {
                _serviceService = serviceService;
                _clinicService = clinicService;
            _dentistDetailService= dentistDetailService;
            _appointmentService = appointmentService;
            }

        [BindProperty]
        public AppointmentRequestModel AppointmentRequest { get; set; }



        public ServiceResponseModel Service { get; set; }
            public List<ClinicResponseModel> Clinics { get; set; }

            public async Task OnGetAsync(int id)
            {
                Service = await _serviceService.GetServiceByIdAsync(id);
                Clinics = await _clinicService.GetClinicsByServiceId(id);
                Dentists = new List<DentistResponseModel>();

            }
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            // Assuming user ID is stored in cookies with key "UserId"
            var userId = User.FindFirstValue("UserId");
            if (int.TryParse(userId, out var customerId))
            {
                await _appointmentService.CreateAppointmentAsync(AppointmentRequest, customerId);
                TempData["Message"] = "Appointment successfully created.";

                return RedirectToPage("/Index");
            }

            ModelState.AddModelError(string.Empty, "Unable to retrieve user ID from cookies.");
            return Page();
        }
        public async Task<IActionResult> OnGetDentistsAsync(int clinicId)
        {
            var dentists = await _dentistDetailService.GetDentistsByClinicId(clinicId);
            return new JsonResult(dentists.Select(d => new { userId=d.DentistId, userName=d.DentistName }));
        }
    }
    
}
