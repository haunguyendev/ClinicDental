using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using PRN221.ClinicDental.Business.Common.Interface;
using PRN221.ClinicDental.Business.DTO.Response.ServiceResponse;
using PRN221.ClinicDental.Data.Models;
using PRN221.ClinicDental.Services.Interfaces;

namespace PRN221.ClinicDental.Presentation.Pages.ClinicOwner.ManageClinic.ManangeClinicService
{
    public class IndexModel : PageModel
    {
        private readonly IServiceService _serviceService;
        private readonly IClinicService _clinicService;

        public IndexModel(IServiceService serviceService, IClinicService clinicService)
        {
            _serviceService = serviceService;
            _clinicService = clinicService;
        }

        public IList<ServiceResponseModel> ClinicService { get; set; } = default!;
        public Clinic Clinic { get; set; }

        public async Task OnGetAsync(int? id)
        {
            Clinic = await _clinicService.GetClinicByClinicId(id);
            ClinicService = await _serviceService.GetServicesByClinicId(id.Value);
        }
        public async Task<IActionResult> OnGetLogout()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToPage("/Accounts/Login");
        }
    }
}
