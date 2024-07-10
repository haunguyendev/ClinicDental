using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using PRN221.ClinicDental.Business.DTO.Request;
using PRN221.ClinicDental.Business.DTO.Response;
using PRN221.ClinicDental.Services.Interfaces;
using System.Threading.Tasks;

namespace PRN221.ClinicDental.Presentation.Pages.Accounts
{
    public class ProfileModel : PageModel
    {
        private readonly IUserService _userService;
        public UserProfileResponse UserProfile { get; set; }

        public ProfileModel(IUserService userService)
        {
            _userService = userService;
        }

        public async Task<IActionResult> OnGetAsync()
        {
            var userId = 1; // Replace with actual logic to get the logged-in user ID
            UserProfile = await _userService.GetUserProfileAsync(userId);
            if (UserProfile == null)
            {
                return NotFound();
            }

            return Page();
        }

        public async Task<IActionResult> OnPostEditProfileAsync(UserProfileUpdateRequest request)
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            try
            {
                await _userService.UpdateUserProfileAsync(request);
                TempData["SuccessMessage"] = "Profile updated successfully.";
            }
            catch (System.Exception ex)
            {
                ModelState.AddModelError(string.Empty, ex.Message);
                return Page();
            }

            return RedirectToPage();
        }

        public async Task<IActionResult> OnPostChangePasswordAsync(int userId, string currentPassword, string newPassword, string confirmPassword)
        {
            if (!ModelState.IsValid || newPassword != confirmPassword)
            {
                ModelState.AddModelError(string.Empty, "Passwords do not match.");
                return Page();
            }

            try
            {
                var result = await _userService.ChangeUserPasswordAsync(userId, currentPassword, newPassword);
                if (result)
                {
                    TempData["SuccessMessage"] = "Password changed successfully.";
                }
            }
            catch (System.Exception ex)
            {
                ModelState.AddModelError(string.Empty, ex.Message);
                return Page();
            }

            return RedirectToPage();
        }
    }
}
