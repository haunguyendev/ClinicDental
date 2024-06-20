using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using PRN221.ClinicDental.Business.Common.Interface;
using PRN221.ClinicDental.Business.DTO.Response.ServiceResponse;

namespace PRN221.ClinicDental.Presentation.Pages.Customer
{
    public class ServicesModel : PageModel
    {
        private readonly IServiceService _serviceService;
        public ServicesModel(IServiceService serviceService)
        {
            _serviceService = serviceService;
            
        }
        public List<ServiceResponseModel> Services { get; set; }
        public async Task OnGetAsync()
        {
            Services = await _serviceService.GetAllListServices();
        }
    }
}
