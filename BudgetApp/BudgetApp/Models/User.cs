using Microsoft.AspNetCore.Identity;

namespace BudgetApp.Models
{
    public class User : IdentityUser
    {
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public virtual ICollection<Account>? Accounts { get; set; }
    }
}
