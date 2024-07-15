using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using PRN221.ClinicDental.Business.Common.Interface;
using PRN221.ClinicDental.Data.Models;

namespace PRN221.ClinicDental.Presentation.Pages.ClinicOwner.ManageClinic.ManageClinicService
{
    public class DeleteClinicServiceModel : PageModel
    {
        private readonly IServiceService _serviceService;

        public DeleteClinicServiceModel(IServiceService serviceService)
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
            else
            {
                ClinicService = clinicservice;
            }
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            await _serviceService.DeleteClinicServices(id.Value);


            return Redirect("/ClinicOwner/ManageClinic");
        }
    }
}
