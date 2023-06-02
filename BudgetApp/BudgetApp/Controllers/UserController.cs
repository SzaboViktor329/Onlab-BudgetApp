using BudgetApp.Models;
using BudgetApp.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace BudgetApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly UserManager<User> userManager;

        public UserController(UserManager<User> userManager)
        {
            this.userManager = userManager;
        }

        [HttpGet]
        public async Task<UserViewModel?> GetUser(string id)
        {
            id = HttpContext.User.Claims.FirstOrDefault(c => c.Type == "user_id")?.Value.ToString() ?? string.Empty;
            if(String.IsNullOrEmpty(id))
            {
                return null;
            }
            var user = await userManager.FindByIdAsync(id);
            if (user == null)
            {
                return null;
            }
            UserViewModel userViewModel = new UserViewModel() {
                Id= user.Id,
                UserName= user.UserName,
                Email= user.Email,
                PhoneNumber= user.PhoneNumber,
                FirstName= user.FirstName,
                LastName= user.LastName
            };
            return userViewModel;
        }
    }
}
