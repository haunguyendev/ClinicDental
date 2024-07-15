using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Azure;
using Microsoft.AspNetCore.Authorization;
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
    [Authorize(Roles = "ClinicOwner")]
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

        public async Task<IActionResult> OnGet()
        {
            var service = await _serviceService.GetAllListServices();
            ViewData["Services"] = new SelectList(service, "ServiceId", "ServiceName");
            ViewData["Districts"] = GetDistricts();
            return Page();
        }

        [BindProperty]
        public ClinicReqModel Clinic { get; set; } = default!;

        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                var service = await _serviceService.GetAllListServices();
                ViewData["Services"] = new SelectList(service, "ServiceId", "ServiceName");
                ViewData["Districts"] = GetDistricts();
                return Page();
            }
            List<Service> selectedServices = await _serviceService.GetServiceByListIdAsync(Clinic.ServiceId);

            var initClinic = new ClinicReqModel()
            {
                Address = Clinic.Address,
                District = Clinic.District,
                Name = Clinic.Name,
                ClinicServices = selectedServices,
                ServiceId = Clinic.ServiceId,
                StreetAddress = Clinic.StreetAddress,
                ImageURL = Clinic.ImageURL
            };
            var userId = User.FindFirstValue("UserId");
            if (int.TryParse(userId, out var customerId))
            {
                var result = await _clinicService.AddClinic(Clinic, customerId);
               
                return RedirectToPage("/ClinicOwner/Index");
            }
            //error
        
            ModelState.AddModelError(string.Empty, "Unable to retrieve user ID from cookies.");

            return RedirectToPage(".ManageClinic/Index");
        }

        private List<SelectListItem> GetDistricts()
        {
            return new List<SelectListItem>
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
            // Thêm các quận khác ở đây
        };
        }
    }
}
