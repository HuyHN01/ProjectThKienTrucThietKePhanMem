using ASC.Utilities;
using ASC.Web.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.WebUtilities;
using System.Text;

namespace ASC.Web.Areas.Identity.Pages.Account
{
    public class InitiateResetPasswordModel : PageModel
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly IEmailSender _emailSender;

        public InitiateResetPasswordModel(UserManager<IdentityUser> userManager, IEmailSender emailSender)
        {
            _userManager = userManager;
            _emailSender = emailSender;
        }

        public void OnGet()
        {
        }

        public async Task<IActionResult> OnPostAsync()
        {
            // Find User
            var userEmail = HttpContext.User.GetCurrentUserDetails().Email;
            var user = await _userManager.FindByEmailAsync(userEmail);

            // Create password reset token
            var code = await _userManager.GeneratePasswordResetTokenAsync(user);

            var encodedCode = WebEncoders.Base64UrlEncode(Encoding.UTF8.GetBytes(code)); // Do Token trong InitiateResetPassword.cshtml.cs có sử dụng cơ chế giải, mã mà muốn giải mã, thì token phải mã hóa

            var callbackUrl = Url.Page(
                "/Account/ResetPassword",
                pageHandler: null,
                values: new { userId = user.Id, code = encodedCode },
                protocol: Request.Scheme);

            // Send email
            await _emailSender.SendEmailAsync(userEmail, "Reset Password", $"Please reset your password by clicking here: <a href='{callbackUrl}'>link</a>");
            return RedirectToPage("./ResetPasswordEmailConfirmation");
        }
    }
}

