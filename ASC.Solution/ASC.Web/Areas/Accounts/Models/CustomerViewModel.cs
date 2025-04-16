using Microsoft.AspNetCore.Identity;

namespace ASC.Web.Areas.Accounts.Models
{
    // 5 references | 0 changes | 0 authors, 0 changes
    public class CustomerViewModel
    {
        public List<IdentityUser>? Customers { get; set; }
        public CustomerRegistrationViewModel Registration { get; set; }
    }
}