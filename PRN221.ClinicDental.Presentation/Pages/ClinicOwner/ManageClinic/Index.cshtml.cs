using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using PRN221.ClinicDental.Business.DTO.Response.Clinic;
using PRN221.ClinicDental.Data.Models;
using PRN221.ClinicDental.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;

namespace PRN221.ClinicDental.Presentation.Pages.ClinicOwner.ManageClinic
{
    [Authorize(Roles = "ClinicOwner")]
    public class IndexModel : PageModel
    {
        private readonly IClinicService _clinicService;

        public IndexModel(IClinicService clinicService)
        {
            _clinicService = clinicService;
        }

        public IList<Clinic> Clinic { get; set; } = default!;

        public async Task OnGetAsync()
        {
            var userId = User.FindFirstValue("UserId");
            if (int.TryParse(userId, out var customerId))
            {
                Clinic = await _clinicService.GetClinicsByClinicOwnerId(customerId);
            }
        }
        public async Task<IActionResult> OnGetLogout()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToPage("/Accounts/Login");
        }
    }

}
