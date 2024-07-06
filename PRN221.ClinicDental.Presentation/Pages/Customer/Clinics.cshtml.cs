using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using PRN221.ClinicDental.Business.DTO.Response.Clinic;
using PRN221.ClinicDental.Business.Helper;
using PRN221.ClinicDental.Services.Interfaces;
using System.Threading.Tasks;

namespace PRN221.ClinicDental.Presentation.Pages.Customer
{
    public class ClinicsModel : PageModel
    {
        private readonly IClinicService _clinicService;

        public ClinicsModel(IClinicService clinicService)
        {
            _clinicService = clinicService;
        }

        public PaginatedList<ClinicResponseModel> Clinics { get; set; }
        public List<DistrictGroupModel> DistrictGroups { get; set; }

        public async Task OnGetAsync(int pageNumber = 1, int pageSize = 6)
        {
            Clinics = await _clinicService.GetAllClinic(pageNumber, pageSize);
            DistrictGroups = await _clinicService.GetClinicsGroupedByDistrict();
        }

        public async Task<IActionResult> OnPostSearchAsync(string keyword, int pageNumber = 1, int pageSize = 6)
        {
            if (string.IsNullOrEmpty(keyword))
            {
                return RedirectToPage();
            }

            Clinics = await _clinicService.SearchClinicByName(keyword, pageNumber, pageSize);
            DistrictGroups = await _clinicService.GetClinicsGroupedByDistrict();
            return Page();
        }

        public async Task<IActionResult> OnGetFilterByDistrictAsync(string district, int pageNumber = 1, int pageSize = 6)
        {
            if (string.IsNullOrEmpty(district))
            {
                Clinics = await _clinicService.GetAllClinic(pageNumber, pageSize);
            }
            else
            {
                Clinics = await _clinicService.GetClinicsByDistrict(district, pageNumber, pageSize);
            }

            DistrictGroups = await _clinicService.GetClinicsGroupedByDistrict();
            return Page();
        }
    }
}
