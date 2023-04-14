using BudgetApp.Data;
using BudgetApp.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Infrastructure;

namespace BudgetApp.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WeatherForecastController : ControllerBase
    {
        private static readonly string[] Summaries = new[]
        {
        "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
    };

        private readonly ILogger<WeatherForecastController> _logger;
        private readonly ApplicationDBContext _applicationDBContext;

        public WeatherForecastController(ILogger<WeatherForecastController> logger, ApplicationDBContext applicationDBContext)
        {
            _logger = logger;
            _applicationDBContext = applicationDBContext;
        }

        [HttpGet]
        [Authorize]
        public IEnumerable<WeatherForecast> Get()
        {
            return Enumerable.Range(1, 5).Select(index => new WeatherForecast
            {
                Date = DateTime.Now.AddDays(index),
                TemperatureC = Random.Shared.Next(-20, 55),
                Summary = Summaries[Random.Shared.Next(Summaries.Length)]
            })
            .ToArray();
        }

        [HttpGet]
        [Route("users")]
        public List<Account> accessDatabase()
        {
            using(var context = _applicationDBContext)
            {
                
                //User? user = context.Users.Find("b5105741-385e-448d-be57-e90bb1b50ec8");
                /*
                List<User> users = context.Users.ToList();
                Account account = new Account();
                account.AccountNumber = "222-333";
                account.AccountName = "raif";
                account.User= users.First();
                context.Accounts.Add(account);
                context.SaveChanges();
                return null;
                */
                return context.Accounts.ToList();
            }

        }
    }
}