using BudgetApp.Data.Repository.RepoServices;
using BudgetApp.Models;
using BudgetApp.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace BudgetApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly IAccountRepository accountRepository;
        private readonly UserManager<User> userManager;

        public AccountController(IAccountRepository accountRepository, UserManager<User> userManager)
        {
            this.accountRepository = accountRepository;
            this.userManager = userManager;
        }

        [HttpGet]
        public List<AccountViewModel> GetAccounts(string userId)
        {
            userId = HttpContext.User.Claims.FirstOrDefault(c => c.Type == "user_id")?.Value.ToString() ?? string.Empty;
            if(String.IsNullOrEmpty(userId))
            {
                return new List<AccountViewModel>();
            }
            List<AccountViewModel> accounts = new List<AccountViewModel>();
            foreach(var account in accountRepository.GetAccountsOfUser(userId))
            {
                accounts.Add(new AccountViewModel() {
                    AccountID = account.AccountID,
                    AccountName= account.AccountName,
                    AccountNumber= account.AccountNumber
                });
            }
            return accounts;
        }

        [HttpPost]
        [Route("create")]
        public async Task<IActionResult> CreateAccount(AccountViewModel accountViewModel, string userId)
        {
            userId = HttpContext.User.Claims.FirstOrDefault(c => c.Type == "user_id")?.Value.ToString() ?? string.Empty;
            if(String.IsNullOrEmpty(userId))
            {
                return BadRequest("User not authenticated");
            }
            var user = await userManager.FindByIdAsync(userId);
            if (user == null)
            {
                return BadRequest("User not exist");
            }
            var accounts = accountRepository.GetAll();
            foreach(var acc in accounts)
            {
                if(acc.AccountNumber.Equals(accountViewModel.AccountNumber) 
                    && acc.AccountName.Equals(accountViewModel.AccountName))
                {
                    return BadRequest("Account already exist");
                }
            }
            Account account = new Account() {
                AccountName= accountViewModel.AccountName,
                AccountNumber= accountViewModel.AccountNumber,
                User = user
            };
            accountRepository.Add(account);
            return Ok();
        }
    }
}
