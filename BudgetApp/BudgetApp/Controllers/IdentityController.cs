using BudgetApp.Models;
using BudgetApp.Services.TokenGenerator;
using BudgetApp.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace BudgetApp.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class IdentityController : ControllerBase
    {

        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;
        private readonly IJWTTokenGenerator _jwtToken;

        public IdentityController(UserManager<User> userManager, SignInManager<User> signInManager, IJWTTokenGenerator jwtToken)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _jwtToken = jwtToken;
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginModel model)
        {

            var userFromDb = await _userManager.FindByNameAsync(model.Username);

            if (userFromDb == null)
            {
                return BadRequest();
            }

            var result = await _signInManager.CheckPasswordSignInAsync(userFromDb, model.Password, false);


            if (!result.Succeeded)
            {
                return BadRequest();
            }
            return Ok(new
            {
                result = result,
                username = userFromDb.UserName,
                email = userFromDb.Email,
                token = _jwtToken.GenerateToken(userFromDb)
            });
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register(RegisterModel model)
        {

            var userToCreate = new User
            {
                Email = model.Email,
                UserName = model.Username
            };

            //Create User
            var result = await _userManager.CreateAsync(userToCreate, model.Password);

            if (result.Succeeded)
            {
                return Ok(result);
            }

            return BadRequest(result);
        }

        /*
        [HttpPost("confirmemail")]
        public IActionResult ConfirmEmail(ConfirmEmailViewModel model)
        {
            return Ok();
        }
        */
    }
}
