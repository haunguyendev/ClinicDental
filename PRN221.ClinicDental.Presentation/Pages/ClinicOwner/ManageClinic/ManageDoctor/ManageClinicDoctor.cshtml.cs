using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using PRN221.ClinicDental.Data.Models;
using PRN221.ClinicDental.Services.Interfaces;

namespace PRN221.ClinicDental.Presentation.Pages.ClinicOwner.ManageDoctor
{
    public class ManageClinicDoctorModel : PageModel
    {
        private readonly IDentistDetailService _dentistDetailService;
        private readonly IClinicService _clinicService;

        public ManageClinicDoctorModel(IDentistDetailService dentistDetailService, IClinicService clinicService)
        {
            _dentistDetailService = dentistDetailService;
            _clinicService = clinicService;
        }

        public IList<DentistDetail> DentistDetail { get; set; } = default!;
        public Clinic DentalClinic { get; set; } = default!;

        public async Task OnGetAsync(int id)
        {
            DentalClinic = await _clinicService.GetClinicByClinicId(id);
            DentistDetail = await _dentistDetailService.GetDentistDetailsByClinicId(id);
        }
        public async Task<IActionResult> OnGetLogout()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToPage("/Accounts/Login");
        }
    }
}
