using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using PRN221.ClinicDental.Business.Common.Interface;
using PRN221.ClinicDental.Business.DTO.Request.Dentist;
using PRN221.ClinicDental.Business.DTO.Request.Dentist;
using PRN221.ClinicDental.Data.Models;
using PRN221.ClinicDental.Services.Interfaces;

namespace PRN221.ClinicDental.Presentation.Pages.ClinicOwner
{
    public class CreateDoctorModel : PageModel
    {
        private readonly IUserService _userService;
        private readonly IRoleService _roleService;
        private readonly IServiceService _serviceService;

        public CreateDoctorModel(IUserService userService, IRoleService roleService, IServiceService serviceService)
        {
            _userService = userService;
            _roleService = roleService;
            _serviceService = serviceService;
        }

        public async Task<IActionResult> OnGet(int clinicId)
        {
            ClinicId=clinicId;
            var service = await _serviceService.GetServicesByClinicId(clinicId);
            ViewData["Services"] = new SelectList(service, "ServiceId", "ServiceName");
            return Page();
        }

        [BindProperty]
        public DentistReqModel Dentist { get; set; } = default!;
        [BindProperty]
        public int ClinicId { get; set; }   

        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public async Task<IActionResult> OnPostAsync(int clinicId)
        {
            var service = await _serviceService.GetServiceByClinicId(ClinicId);
            if (!ModelState.IsValid)
            {
                ViewData["Services"] = new SelectList(service, "ServiceId", "ServiceName");
                return Page();
            }

            var temp = await _userService.GetUserByEmail(Dentist.Email);
            if (temp != null)
            {
                ModelState.AddModelError(string.Empty, "Email already used");
                ViewData["Services"] = new SelectList(service, "ServiceId", "ServiceName");
                return Page();
            }

            var result = await _userService.GetUserByUserName(Dentist.Username);
            if (result != null)
            {
                    ModelState.AddModelError(string.Empty, "UserName already used");
                ViewData["Services"] = new SelectList(service, "ServiceId", "ServiceName");
                return Page();
            }

            await _userService.CreateDentist(Dentist, clinicId);


            return Redirect($"/ClinicOwner/ManageClinic/ManageDoctor/ManageClinicDoctor?id={clinicId}");
        }
    }
}
