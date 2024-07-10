using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using PRN221.ClinicDental.Business.DTO.Response.User;
using PRN221.ClinicDental.Business.Helper;
using PRN221.ClinicDental.Services;
using PRN221.ClinicDental.Services.Interfaces;

namespace PRN221.ClinicDental.Presentation.Pages.Admin
{
    public class DashboardModel : PageModel
    {
        private readonly IUserService _userService;

        public DashboardModel(IUserService userService)
        {
            _userService = userService;
        }
        public PaginatedList<UserResponseModel> Users { get; set; }
        public int CurrentPage { get; set; }
        public int TotalPages { get; set; }
        [BindProperty(SupportsGet = true)]
        public string SearchString { get; set; }

        public async Task OnGetAsync(int pageNumber = 1)
        {
            int pageSize = 8; // Number of items per page
            Users = await _userService.GetPaginatedUsersAsync(pageNumber, pageSize);
            CurrentPage = Users.CurrentPage;
            TotalPages = Users.TotalPages;
        }
        public async Task OnPostSearchAsync(string searchString, int pageNumber = 1)
        {
            int pageSize = 8; // Number of items per page
            Users = await _userService.SearchUsersAsync(SearchString, pageNumber, pageSize);
            CurrentPage = Users.CurrentPage;
            TotalPages = Users.TotalPages;
        }
    }
}
