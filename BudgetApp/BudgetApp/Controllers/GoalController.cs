using BudgetApp.Data.Repository.RepoServices;
using BudgetApp.Models;
using BudgetApp.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BudgetApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GoalController : ControllerBase
    {
        private readonly IGoalRepository goalRepository;
        private readonly IAccountRepository accountRepository;
        private readonly ITransactionRepository transactionRepository;

        public GoalController(IGoalRepository goalRepository, IAccountRepository accountRepository, ITransactionRepository transactionRepository)
        {
            this.goalRepository = goalRepository;
            this.accountRepository = accountRepository;
            this.transactionRepository = transactionRepository;
        }

        [HttpPost]
        public IActionResult AddGoal(GoalViewModel goalViewModel, int accountId)
        {
            var account = accountRepository.GetById(accountId);
            if (account == null)
            {
                return BadRequest("Account not exist");
            }
            if (!accountRepository.AccountOfUser(HttpContext.User.Claims.FirstOrDefault(c => c.Type == "user_id").Value.ToString(), accountId))
            {
                return BadRequest("Not the user's account");
            }
            var goals = goalRepository.GetGoalsOfAccount(accountId, goalViewModel.Annual, goalViewModel.GoalDate);
            foreach(var g in goals)
            {
                if(goalViewModel.Category.Equals(g.Category)) {
                    return BadRequest("Goal is already exist");
                }
            }
            Goal goal = new Goal()
            {
                Annual = goalViewModel.Annual,
                Category = goalViewModel.Category,
                GoalDate = goalViewModel.GoalDate,
                TargetAmount = goalViewModel.TargetAmount,
                Account = account
            };
            goalRepository.Add(goal);
            return Ok();
        }

        [HttpGet]
        [Route("availabledates")]
        public List<string> GetAvailableDates(int accountId)
        {
            if (!accountRepository.AccountOfUser(HttpContext.User.Claims.FirstOrDefault(c => c.Type == "user_id").Value.ToString(), accountId))
            {
                return null;
            }
            var dates = goalRepository.GetAvailableYearsMonths(accountId);
            List<string> result = new List<string>();
            foreach (var date in dates)
            {
                result.Add(date.ToString("yyyy-MM-dd"));
            }
            return result;
        }

        [HttpGet]
        [Route("availableyears")]
        public List<string> GetAvailableYears(int accountId)
        {
            if (!accountRepository.AccountOfUser(HttpContext.User.Claims.FirstOrDefault(c => c.Type == "user_id").Value.ToString(), accountId))
            {
                return null;
            }
            var dates = goalRepository.GetAvailableYears(accountId);
            List<string> result = new List<string>();
            foreach (var date in dates)
            {
                result.Add(date.ToString("yyyy-MM-dd"));
            }
            return result;
        }

        [HttpGet]
        public List<GoalViewModel> GetGoals(int accountId, bool annual, DateTime date)
        {
            if (!accountRepository.AccountOfUser(HttpContext.User.Claims.FirstOrDefault(c => c.Type == "user_id").Value.ToString(), accountId))
            {
                return null;
            }
            var goals = goalRepository.GetGoalsOfAccount(accountId, annual, date);
            var result = new List<GoalViewModel>();
            foreach (var goal in goals)
            {
                result.Add(new GoalViewModel()
                {
                    GoalId= goal.GoalId,
                    Annual= goal.Annual,
                    Category= goal.Category,
                    GoalDate= goal.GoalDate,
                    TargetAmount= goal.TargetAmount,
                    ActualAmount= Math.Abs(transactionRepository.GetSumByCategory(accountId, annual, goal.GoalDate, goal.Category))
                });
            }

            return result;
        }
    }
}
