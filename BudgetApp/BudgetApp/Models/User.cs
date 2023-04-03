using Microsoft.AspNetCore.Identity;

namespace BudgetApp.Models
{
    public class User : IdentityUser
    {
        public string? FavColor { get; set; }
        public string? FavAnimal { get; set; }
    }
}
