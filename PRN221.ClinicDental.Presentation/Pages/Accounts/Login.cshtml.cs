using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using PRN221.ClinicDental.Data.Common.Interface;

namespace PRN221.ClinicDental.Presentation.Pages.Accounts
{
    public class LoginModel : PageModel
    {
        private readonly ILogger<LoginModel> _logger;
        private readonly IUnitOfWork _unitOfWork;

        [BindProperty]
        public string Username { get; set; }

        [BindProperty]
        public string Password { get; set; }

        public LoginModel(ILogger<LoginModel> logger,IUnitOfWork unitOfWork)
        {
            _logger = logger;
            _unitOfWork = unitOfWork;
        }

        public void OnGet()
        {
        }

        public async Task<IActionResult> OnPostAsync()
        {
            var user = await _unitOfWork.UserRepository.GetUserByUserAndPassword(Username, Password);
            if(user!=null)
                return RedirectToPage("/Index");

            return NotFound();
        }
        
    }
}
