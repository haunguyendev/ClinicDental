using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Azure;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using PRN221.ClinicDental.Business.Common.Interface;
using PRN221.ClinicDental.Data.Models;
using PRN221.ClinicDental.Services.Interfaces;

namespace PRN221.ClinicDental.Presentation.Pages.ClinicOwner.ManageClinic.ManageClinicService
{
    public class CreateClinicServiceModel : PageModel
    {
        private readonly IServiceService _serviceService;
        private readonly IClinicService _clinicService;

        public CreateClinicServiceModel(IClinicService clinicService, IServiceService serviceService)
        {
            _serviceService = serviceService;
            _clinicService = clinicService;
        }

        public async Task<IActionResult> OnGet(int? id)
        {
            Clinic = await _clinicService.GetClinicByClinicId(id);
            var service = await _serviceService.GetAllListServicesForCreate();
            var list = await _serviceService.GetServicesByClinicId(id.Value);

            List<Service> temp = service.Where(x => !(list.Select(y => y.ServiceId).ToList().Contains(x.ServiceId))).ToList();
            ViewData["Service"] = new SelectList(temp, "ServiceId", "ServiceName");
            return Page();
        }

        [BindProperty]
        public ClinicService ClinicService { get; set; } = default!;
        public Clinic Clinic { get; set; }
        public async Task<IActionResult> OnPostAsync(int? id)
        {
            //if (!ModelState.IsValid)
            //{
            //    return Page();
            //}

            await _serviceService.CreateClinicService(ClinicService.Service.ServiceId, id.Value, ClinicService.Price);

            return Redirect("/ClinicOwner/ManageClinic");
        }
    }
}
