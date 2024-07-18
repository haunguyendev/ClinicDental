using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using PRN221.ClinicDental.Business.DTO.Response.Dentist;
using PRN221.ClinicDental.Data.Models;
using PRN221.ClinicDental.Services.Interfaces;

namespace PRN221.ClinicDental.Presentation.Pages.ClinicOwner.ManageClinic
{
    [Authorize(Roles = "ClinicOwner")]
    public class ManageClinicDoctorModel : PageModel
    {
        private readonly IDentistDetailService _dentistDetailService;

        public ManageClinicDoctorModel(IDentistDetailService dentistDetailService)
        {
            _dentistDetailService   = dentistDetailService;
        }

        public IList<DentistDetail> DentistDetail { get;set; } = default!;

        public async Task OnGetAsync(int id)
        {
            DentistDetail = await _dentistDetailService.GetDentistDetailsByClinicId(id);
        }
        public async Task<IActionResult> OnGetLogout()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToPage("/Accounts/Login");
        }
    }
}
