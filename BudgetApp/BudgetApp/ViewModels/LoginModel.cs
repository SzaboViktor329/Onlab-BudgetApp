using System.ComponentModel.DataAnnotations;

namespace BudgetApp.ViewModels
{
    public class LoginModel
    {
        [Required]
        public string Username { get; set; }
        [Required]
        public string Password { get; set; }
    }
}
