using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using PRN221.ClinicDental.Business.Common.Interface;
using PRN221.ClinicDental.Business.DTO.Request.Dentist;
using PRN221.ClinicDental.Data.Models;
using PRN221.ClinicDental.Services.Interfaces;

namespace PRN221.ClinicDental.Presentation.Pages.ClinicOwner.ManageDoctor
{
    public class EditDentistModel : PageModel
    {
        private readonly IUserService _userService;
        private readonly IServiceService _serviceService;

        public EditDentistModel(IUserService userService, IServiceService serviceService)
        {
            _userService = userService;
            _serviceService = serviceService;
        }

        [BindProperty]
        public DentistUpdateReqModel Dentist { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var user = await _userService.GetDentistById(id.Value);
            if (user == null)
            {
                return NotFound();
            }
            Dentist = new DentistUpdateReqModel
            {
                ID = id.Value,
                FullName = user.Name,
                PhoneNumber = user.PhoneNumber ?? null,
                Address = user.Address ?? null,
                Certificate = user.DentistDetail.Certificate ?? null,
                Qualification = user.DentistDetail.Qualification ?? null,
                Experience = user.DentistDetail.Experience ?? null,
                ServiceId = user.DentistDetail.DentistServices.Select(s => s.ServiceId).ToList(),
                DentistService = user.DentistDetail.DentistServices.ToList(),
            };
            ViewData["ServiceId"] = new SelectList(await _serviceService.GetAllListServices(), "ServiceId", "ServiceName", Dentist.ServiceId);
            return Page();
        }

        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync(int clinicId)
        {
            if (!ModelState.IsValid)
            {
                ViewData["ServiceId"] = new SelectList(await _serviceService.GetAllListServices(), "ServiceId", "ServiceName", Dentist.ServiceId);
                return Page();
            }
            await _userService.UpdateDentist(Dentist);
            return Redirect($"/ClinicOwner/ManageClinic/ManageDoctor/ManageClinicDoctor?id={clinicId}");
        }


    }
}
