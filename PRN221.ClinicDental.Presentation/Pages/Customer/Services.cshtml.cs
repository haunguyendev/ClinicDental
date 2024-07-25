using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using PRN221.ClinicDental.Business.Common.Interface;
using PRN221.ClinicDental.Business.DTO.Response.ServiceResponse;
using PRN221.ClinicDental.Data.Models;
using Microsoft.AspNetCore.Authorization;

namespace PRN221.ClinicDental.Presentation.Pages.Customer
{
    [Authorize(Roles = "Customer")]
    public class ServicesModel : PageModel
    {
        private readonly IServiceService _serviceService;
        public ServicesModel(IServiceService serviceService)
        {
            _serviceService = serviceService;
            
        }
        public List<ServiceResponseModel> Services { get; set; }
        public int ClinicId { get; set; }
        public async Task OnGetAsync(int clinicId)
        {
            ClinicId = clinicId;
            Services = await _serviceService.GetServicesByClinicId(clinicId);
        }
        public async Task<IActionResult> OnGetLogout()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToPage("/Accounts/Login");
        }

    }
}
