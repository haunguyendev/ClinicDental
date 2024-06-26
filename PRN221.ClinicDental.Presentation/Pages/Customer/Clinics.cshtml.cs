using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using PRN221.ClinicDental.Business.DTO.Response.Clinic;
using PRN221.ClinicDental.Services.Interfaces;

namespace PRN221.ClinicDental.Presentation.Pages.Customer
{
    public class ClinicsModel : PageModel
    {
        private readonly IClinicService _clinicService;
        public ClinicsModel(IClinicService clinicService)
        {
            _clinicService = clinicService;
            
        }
        public IList<ClinicResponseModel> Clinics { get; set; }
        public List<DistrictGroupModel> DistrictGroups { get; set; }
        public async Task OnGetAsync()
        {
            Clinics = await _clinicService.GetAllClinic();
            DistrictGroups = await _clinicService.GetClinicsGroupedByDistrict();
        }
        public async Task<IActionResult> OnPostSearchAsync(string keyword)
        {
            if (string.IsNullOrEmpty(keyword))
            {
                return RedirectToPage(); 
            }

            Clinics = await _clinicService.SearchClinicByName(keyword);
            return Page(); 
        }
    }
}

