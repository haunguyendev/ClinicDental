using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using PRN221.ClinicDental.Business.DTO.Response.User;
using PRN221.ClinicDental.Business.Helper;
using PRN221.ClinicDental.Services;
using PRN221.ClinicDental.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;

namespace PRN221.ClinicDental.Presentation.Pages.Admin
{
    [Authorize(Roles = "Admin")]
    public class DashboardModel : PageModel
    {
        private readonly IUserService _userService;
        private readonly IAppointmentService _appointmentService;
        private readonly IClinicService _clinicService;

        public DashboardModel(IUserService userService,IAppointmentService appointmentService,IClinicService clinicService)
        {
            _userService = userService;
            _appointmentService = appointmentService;
            _clinicService = clinicService;
        }
        public PaginatedList<UserResponseModel> Users { get; set; }
        public int CurrentPage { get; set; }
        public int TotalPages { get; set; }
        [BindProperty(SupportsGet = true)]
        public string SearchString { get; set; }
        public int TotalPatients { get; set; }
        public int TotalClinicOwners { get; set; }
        public int TotalDentists { get; set; }
        public int TotalClinics { get; set; }
        public decimal TotalMonthlyRevenue { get; set; }
        public int TotalCompletedAppointmentsThisWeek { get; set; }

        public async Task OnGetAsync(int pageNumber = 1)
        {
            int pageSize = 8; // Number of items per page
            Users = await _userService.GetPaginatedUsersAsync(pageNumber, pageSize);
            CurrentPage = Users.CurrentPage;
            TotalPages = Users.TotalPages;
            TotalDentists = await _userService.GetTotalDentistsAsync();
            TotalClinicOwners = await _userService.GetTotalClinicOwnersAsync();
            TotalPatients = await _userService.GetTotalPatientsAsync();
            TotalClinics = await _clinicService.GetTotalClinicsAsync();
            TotalMonthlyRevenue= await _appointmentService.GetMonthlyRevenueAsync();
            TotalCompletedAppointmentsThisWeek = await _appointmentService.GetTotalCompletedAppointmentsThisWeekAsync(); 
        }
        public async Task OnPostSearchAsync(string searchString, int pageNumber = 1)
        {
            int pageSize = 8; // Number of items per page
            Users = await _userService.SearchUsersAsync(SearchString, pageNumber, pageSize);
            CurrentPage = Users.CurrentPage;
            TotalPages = Users.TotalPages;
        }
        public async Task<IActionResult> OnGetLogout()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToPage("/Accounts/Login");
        }
    }
}
