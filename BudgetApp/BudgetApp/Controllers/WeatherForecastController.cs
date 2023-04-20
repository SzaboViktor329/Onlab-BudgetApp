using BudgetApp.Data;
using BudgetApp.Data.Repository.RepoServices;
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
        private readonly ITransactionRepository transactionRepository;

        public WeatherForecastController(ILogger<WeatherForecastController> logger, ITransactionRepository transactionRepository)
        {
            _logger = logger;
            this.transactionRepository = transactionRepository;
        }

        [HttpGet]
        public IEnumerable<WeatherForecast> Get()
        {
            var dates = transactionRepository.getAvailableYearsMonths(3);
            var expenses = transactionRepository.GetExpenses(3, true, new DateTime(2023, 2, 1));
            var piecharts = transactionRepository.GetReportByCategories(3, false, new DateTime(2023, 2, 1));
            int m = 10;
            return Enumerable.Range(1, 5).Select(index => new WeatherForecast
            {
                Date = DateTime.Now.AddDays(index),
                TemperatureC = Random.Shared.Next(-20, 55),
                Summary = Summaries[Random.Shared.Next(Summaries.Length)]
            })
            .ToArray();
        }
        /*
        [HttpGet]
        [Route("users")]
        public Account accessDatabase([FromServices] IAccountRepository repository)
        {
            return repository.GetById(1);
        }
        */
    }
}