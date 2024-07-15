using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using PRN221.ClinicDental.Business.Common.Interface;
using PRN221.ClinicDental.Business.DTO.Response.ServiceResponse;
using PRN221.ClinicDental.Data.Models;

namespace PRN221.ClinicDental.Presentation.Pages.ClinicOwner.ManageClinic.ManageClinicService
{
    public class EditClinicServiceModel : PageModel
    {
        private readonly IServiceService _serviceService;

        public EditClinicServiceModel(IServiceService serviceService)
        {
            _serviceService = serviceService;
        }

        [BindProperty]
        public ClinicService ClinicService { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var clinicservice = await _serviceService.GetServiceByClinicServiceId(id.Value);
            if (clinicservice == null)
            {
                return NotFound();
            }
            ClinicService = clinicservice;
            return Page();
        }

        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            //if (!ModelState.IsValid)
            //{
            //    return Page();
            //}
            var newClinicService = await _serviceService.GetServiceByClinicServiceId(ClinicService.ClinicServiceId);

            if (ClinicService.Price != 0)
            {
                newClinicService.Price = ClinicService.Price;
                await _serviceService.UpdatePriceClinicServices(newClinicService);
            }

            return Redirect($"/ClinicOwner/ManageClinic");
        }


    }
}
