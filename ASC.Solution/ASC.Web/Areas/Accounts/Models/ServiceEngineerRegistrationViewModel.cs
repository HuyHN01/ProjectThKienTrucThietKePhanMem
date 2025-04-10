using System.ComponentModel.DataAnnotations;

namespace ASC.Web.Areas.Accounts.Models
{
    // 2 references
    public class ServiceEngineerRegistrationViewModel
    {
        // 4 references
        public string UserName { get; set; }

        [Required]
        [EmailAddress]
        [Display(Name = "Email")]
        // 10 references
        public string Email { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} and at max {1} characters long.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        // 5 references
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Confirm password")]
        [Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
        // 2 references
        public string ConfirmPassword { get; set; }
        // 3 references
        public bool IsEdit { get; set; }
        // 4 references
        public bool IsActive { get; set; }
    }
}