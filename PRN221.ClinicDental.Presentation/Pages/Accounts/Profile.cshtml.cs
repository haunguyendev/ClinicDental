using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using PRN221.ClinicDental.Business.DTO.Request;
using PRN221.ClinicDental.Business.DTO.Response;
using PRN221.ClinicDental.Services.Interfaces;
using System.Security.Claims;
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
        [BindProperty]
        public ChangePasswordRequest ChangePasswordRequest { get; set; }

        public async Task<IActionResult> OnGetAsync()
        {
            var userIdString = User.FindFirstValue("UserId");
            if (int.TryParse(userIdString, out int userId))
            {
                UserProfile = await _userService.GetUserProfileAsync(userId);
                if (UserProfile == null)
                {
                    // Log the userId to verify it
                    Console.WriteLine($"User ID: {userId}");
                    return NotFound();
                }

                return Page();
            }
            else
            {
                return BadRequest("Invalid user ID.");
            }
        }

        public async Task<IActionResult> OnPostEditProfileAsync(UserProfileUpdateRequest request)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return Page();
                }

                var userIdString = User.FindFirstValue("UserId");
                if (!int.TryParse(userIdString, out var userId))
                {
                    ModelState.AddModelError(string.Empty, "Invalid user ID.");
                    return Page();
                }

                // Ensure UserProfileUpdateRequest is initialized
                if (request == null)
                {
                    ModelState.AddModelError(string.Empty, "Profile update request is null.");
                    return Page();
                }

                // Attach the userId to the request
                request.UserId = userId;

                // Log the request details
                Console.WriteLine($"Updating profile for User ID: {userId}");

                // Use the result to handle success or failure
                bool result = await _userService.UpdateUserProfileAsync(request);

                if (result)
                {
                    TempData["SuccessMessage"] = "Profile updated successfully.";
                }
                else
                {
                    ModelState.AddModelError(string.Empty, "Failed to update profile.");
                }

                return RedirectToPage("/Accounts/Profile");
            }
            catch (Exception ex)
            {
                ModelState.AddModelError(string.Empty, $"Error updating profile: {ex.Message}");
                return Page();
            }
        }
        public async Task<IActionResult> OnPostChangePasswordAsync()
        {
            var userIdString = User.FindFirstValue("UserId");
            if (int.TryParse(userIdString, out int userId))
            {
                if (!ModelState.IsValid)
                {
                    
                    var errorMessages = ModelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage).ToArray();
                    TempData["ErrorMessages"] = errorMessages;
                    return RedirectToPage();
                }

                try
                {
                    var result = await _userService.ChangeUserPasswordAsync(userId, ChangePasswordRequest.CurrentPassword, ChangePasswordRequest.NewPassword);
                    if (result)
                    {
                        TempData["SuccessMessage"] = "Password changed successfully.";
                        return RedirectToPage();
                    }
                    else
                    {
                        TempData["ErrorMessages"] = new string[] { "Current password is incorrect." };
                        return RedirectToPage();
                    }
                }
                catch (System.Exception ex)
                {
                    TempData["ErrorMessages"] = new string[] { ex.Message };
                    return RedirectToPage();
                }
            }
            else
            {
                TempData["ErrorMessages"] = new string[] { "Invalid user ID." };
                return RedirectToPage();
            }
        }
        public async Task<IActionResult> OnGetLogout()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToPage("/Accounts/Login");
        }


    }
}
