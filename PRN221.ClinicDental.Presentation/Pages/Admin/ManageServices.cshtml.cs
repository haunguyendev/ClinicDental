using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using PRN221.ClinicDental.Business.Common.Interface;
using PRN221.ClinicDental.Business.DTO.Request.ServiceModel;
using PRN221.ClinicDental.Business.DTO.Response.ServiceResponse;
using PRN221.ClinicDental.Data.Models;
using Microsoft.AspNetCore.Authorization;

namespace PRN221.ClinicDental.Presentation.Pages.Admin
{
    [Authorize(Roles = "Admin")]
    public class ManageServicesModel : PageModel
    {
        private readonly IServiceService _serviceService;

        public ManageServicesModel(IServiceService serviceService)
        {
            _serviceService = serviceService;
        }
        [TempData]
        public string ErrorMessage { get; set; }

        [BindProperty]
        public ServiceViewAdminRequest ServiceRequest { get; set; }

        public List<ServiceViewAdminResponse> Services { get; set; }

        public async Task OnGetAsync()
        {
            Services = await _serviceService.GetAllServicesAdminView();
        }
        public async Task<IActionResult> OnGetServiceAsync(int id)
        {
            var service = await _serviceService.GetServiceByIdAsync(id);
            if (service == null)
            {
                return NotFound();
            }

            return new JsonResult(service);
        }

        public async Task<IActionResult> OnPostCreateAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }
            bool serviceNameExists = await _serviceService.ServiceNameExistsAsync(ServiceRequest.ServiceName);
            if (serviceNameExists)
            {
                ErrorMessage = "Service name already exists.";
                return RedirectToPage();
            }

            await _serviceService.CreateServiceAsync(ServiceRequest);
            return RedirectToPage();
        }

        public async Task<IActionResult> OnPostEditAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            var service = await _serviceService.GetServiceByIdAsync((int)ServiceRequest.ServiceId);
            if (service == null)
            {
                return NotFound();
            }
            

            service.ServiceName = ServiceRequest.ServiceName;
            service.Description = ServiceRequest.Description;
            service.ImageURL = ServiceRequest.ImageURL;

            await _serviceService.UpdateServiceAsync(service);
            return RedirectToPage();
        }

        public async Task<IActionResult> OnPostDeleteAsync(int id)
        {
            try
            {
                bool result = await _serviceService.DeleteServiceAsync(id);
                if (!result)
                {
                    ErrorMessage = "Service not found.";
                    return RedirectToPage();
                }

                return RedirectToPage();
            }
            catch (InvalidOperationException ex)
            {
                ErrorMessage = ex.Message;
                return RedirectToPage();
            }
        }

        public async Task<IActionResult> OnGetLogout()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToPage("/Accounts/Login");
        }


    }
}
