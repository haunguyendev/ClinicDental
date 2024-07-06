// Profile.cshtml.cs
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

        public ProfileModel(IUserService userService)
        {
            _userService = userService;
        }

        [BindProperty]
        public UserProfileResponse UserProfile { get; set; }

        [BindProperty]
        public UserProfileUpdateRequest ProfileUpdateRequest { get; set; }

        [BindProperty]
        public ChangePasswordRequest ChangePasswordRequest { get; set; }

        public async Task<IActionResult> OnGetAsync(int userId)
        {
            try
            {
                UserProfile = await _userService.GetUserProfileAsync(userId); ;
                return Page();
            }
            catch (Exception ex)
            {
                ModelState.AddModelError(string.Empty, $"Error loading profile: {ex.Message}");
                return Page();
            }
        }

        public async Task<IActionResult> OnPostUpdateProfileAsync()
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return Page();
                }

                await _userService.UpdateUserProfileAsync(ProfileUpdateRequest);
                TempData["SuccessMessage"] = "Profile updated successfully.";
                return RedirectToPage();
            }
            catch (Exception ex)
            {
                ModelState.AddModelError(string.Empty, $"Error updating profile: {ex.Message}");
                return Page();
            }
        }

        public async Task<IActionResult> OnPostChangePasswordAsync()
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return Page();
                }

                var userId = int.Parse(User.FindFirst("UserId").Value);
                var success = await _userService.ChangeUserPasswordAsync(userId, ChangePasswordRequest.CurrentPassword, ChangePasswordRequest.NewPassword);

                if (success)
                {
                    TempData["SuccessMessage"] = "Password changed successfully.";
                }
                else
                {
                    TempData["ErrorMessage"] = "Failed to change password.";
                }

                return RedirectToPage();
            }
            catch (Exception ex)
            {
                ModelState.AddModelError(string.Empty, $"Error changing password: {ex.Message}");
                return Page();
            }
        }
    }
}
