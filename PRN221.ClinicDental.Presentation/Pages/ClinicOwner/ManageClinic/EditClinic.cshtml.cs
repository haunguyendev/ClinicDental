using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using PRN221.ClinicDental.Business.Common.Interface;
using PRN221.ClinicDental.Business.DTO.Request.ClinicReqModel;
using PRN221.ClinicDental.Business.DTO.Response.ServiceResponse;
using PRN221.ClinicDental.Data.Models;
using PRN221.ClinicDental.Services.Interfaces;

namespace PRN221.ClinicDental.Presentation.Pages.ClinicOwner.ManageClinic
{
    public class EditClinicModel : PageModel
    {
        private readonly IClinicService _clinicService;
        private readonly IServiceService _serviceService;
        public EditClinicModel(IClinicService clinicService, IServiceService serviceService)
        {
            _clinicService = clinicService;
            _serviceService = serviceService;
        }

        [BindProperty]
        public ClinicReqModel Clinic { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var service = await _serviceService.GetAllListServices();
            var clinic = await _clinicService.GetClinicByClinicId(id);

            Clinic = new ClinicReqModel();
            Clinic.Name = clinic.Name;
            Clinic.StreetAddress = clinic.Address.StreetAddress;
            Clinic.District = clinic.Address.District;
            Clinic.ClinicServices = clinic.ClinicServices.Select(x=> x.Service).ToList();

            ViewData["Districts"] = GetDistricts(Clinic.District);
            ViewData["Services"] = GetService(Clinic.ClinicServices, service);

            if (clinic == null)
            {
                return NotFound();
            }
            return Page();
        }

        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                var service = await _serviceService.GetAllListServices();
                ViewData["Districts"] = GetDistricts(Clinic.District);
                ViewData["Services"] = GetService(Clinic.ClinicServices, service);
                return Page();
            }

     
            return RedirectToPage("./Index");
        }

        private List<SelectListItem> GetDistricts(string selectedValue)
        {
            var list = new List<SelectListItem>
    {
        new SelectListItem { Value = "1", Text = "Quận 1" },
        new SelectListItem { Value = "2", Text = "Quận 2" },
        new SelectListItem { Value = "3", Text = "Quận 3" },
        new SelectListItem { Value = "4", Text = "Quận 4" },
        new SelectListItem { Value = "5", Text = "Quận 5" },
        new SelectListItem { Value = "6", Text = "Quận 6" },
        new SelectListItem { Value = "7", Text = "Quận 7" },
        new SelectListItem { Value = "8", Text = "Quận 8" },
        new SelectListItem { Value = "9", Text = "Quận 9" },
        new SelectListItem { Value = "10", Text = "Quận 10" },
        new SelectListItem { Value = "11", Text = "Quận 11" },
        new SelectListItem { Value = "12", Text = "Quận 12" },
        new SelectListItem { Value = "BinhTan", Text = "Quận Bình Tân" },
        new SelectListItem { Value = "BinhThanh", Text = "Quận Bình Thạnh" },
        new SelectListItem { Value = "GoVap", Text = "Quận Gò Vấp" },
        new SelectListItem { Value = "PhuNhuan", Text = "Quận Phú Nhuận" },
        new SelectListItem { Value = "TanBinh", Text = "Quận Tân Bình" },
        new SelectListItem { Value = "TanPhu", Text = "Quận Tân Phú" },
        new SelectListItem { Value = "ThuDuc", Text = "Quận Thủ Đức" },
        new SelectListItem { Value = "BinhChanh", Text = "Huyện Bình Chánh" },
        new SelectListItem { Value = "CanGio", Text = "Huyện Cần Giờ" },
        new SelectListItem { Value = "CuChi", Text = "Huyện Củ Chi" },
        new SelectListItem { Value = "HocMon", Text = "Huyện Hóc Môn" },
        new SelectListItem { Value = "NhaBe", Text = "Huyện Nhà Bè" }
    };
            foreach (var item in list)
            {
                if (item.Value == selectedValue)
                {
                    item.Selected = true;
                    break;
                }
            }

            return list;
        }

        public List<SelectListItem> GetService(List<Service> selectedValues, List<ServiceResponseModel> services)
        {
            var selectedServiceIds = selectedValues.Select(s => s.ServiceId).ToList();

            var list = services.Select(service => new SelectListItem
            {
                Value = service.ServiceId.ToString(),
                Text = service.ServiceName,
                Selected = selectedServiceIds.Contains(service.ServiceId)
            }).ToList();

            return list;
        }
    }


}
