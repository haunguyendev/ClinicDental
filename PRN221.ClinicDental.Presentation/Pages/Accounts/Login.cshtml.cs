using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using PRN221.ClinicDental.Data.Common.Interface;
using PRN221.ClinicDental.Data.Repositories;
using System.ComponentModel.DataAnnotations;
using System.Security.Claims;
using Microsoft.AspNetCore.Identity;
using PRN221.ClinicDental.Business.DTO.Request;
using PRN221.ClinicDental.Services.Interfaces;

namespace PRN221.ClinicDental.Presentation.Pages.Accounts
{
    public class LoginModel : PageModel
    {
        private readonly ILogger<LoginModel> _logger;

        private IUserService _userService;
        [BindProperty]
        public UserLoginRequest UserLogin { get; set; }

        public LoginModel(ILogger<LoginModel> logger,IUserService userService)
        {
            _logger = logger;
            _userService = userService;
          }

        public void OnGet()
        {
        }

        public async Task<IActionResult> OnPostLoginAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            var user = await _userService.Authenticate(UserLogin.Username, UserLogin.Password);


            if (user == null)
            {
                ModelState.AddModelError(string.Empty, "Invalid username or password.");
                return Page();
            }

            var claims = new List<Claim>
            {
                 new Claim("UserId", user.UserId.ToString()),
                new Claim(ClaimTypes.Name, user.Name),
                new Claim(ClaimTypes.Email, user.Email),
                new Claim(ClaimTypes.Role, user.Role)
            };

            var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
            var authProperties = new AuthenticationProperties { IsPersistent = true };
            foreach (var claim in claimsIdentity.Claims)
            {
                Console.WriteLine($"{claim.Type}: {claim.Value}");
            }
            await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme,
                                          new ClaimsPrincipal(claimsIdentity),
                                          authProperties);
            return Redirect("/Index");
        }

    }
}
