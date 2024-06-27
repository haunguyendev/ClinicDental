﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using PRN221.ClinicDental.Business.Common.Interface;
using PRN221.ClinicDental.Business.DTO.Request.ClinicReqModel;
using PRN221.ClinicDental.Business.DTO.Response.Clinic;
using PRN221.ClinicDental.Data.Models;
using PRN221.ClinicDental.Services;
using PRN221.ClinicDental.Services.Interfaces;

namespace PRN221.ClinicDental.Presentation.Pages.ClinicOwner.ManageClinic
{
    public class CreateClinicModel : PageModel
    {
        private readonly IClinicService _clinicService;
        private readonly IUserService _userService;
        private readonly IServiceService _serviceService;

        public CreateClinicModel(IClinicService clinicService, IUserService userService, IServiceService serviceService)
        {
            _clinicService = clinicService;
            _userService = userService;
            _serviceService = serviceService;
        }

        public async  Task<IActionResult> OnGet()
        {
            var service = await _serviceService.GetAllListServices();
            ViewData["Service"] = new SelectList(service, "ServiceId", "ServiceName");

            return Page();
        }

        [BindProperty]
        public ClinicReqModel Clinic { get; set; } = default!;

        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            var userId = User.FindFirstValue("UserId");
            if (int.TryParse(userId, out var customerId))
            {
                var result = await _clinicService.AddClinic( Clinic , customerId);
                TempData["Message"] = "Appointment successfully created.";

                return RedirectToPage("/Index");
            }

            ModelState.AddModelError(string.Empty, "Unable to retrieve user ID from cookies.");

            return RedirectToPage("./Index");
       }
    }
}