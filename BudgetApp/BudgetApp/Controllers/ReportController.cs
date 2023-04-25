using BudgetApp.Data.Repository.RepoServices;
using BudgetApp.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Globalization;

namespace BudgetApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReportController : ControllerBase
    {
        private readonly ITransactionRepository transactionRepository;

        public ReportController(ITransactionRepository transactionRepository)
        {
            this.transactionRepository = transactionRepository;
        }

        [HttpGet]
        [Route("availabledates")]
        public List<string> GetAvailableDates(int accountId)
        {
            var dates = transactionRepository.GetAvailableYearsMonths(accountId);
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
            var dates = transactionRepository.GetAvailableYears(accountId);
            List<string> result = new List<string>();
            foreach (var date in dates)
            {
                result.Add(date.ToString("yyyy-MM-dd"));
            }
            return result;
        }

        [HttpGet]
        [Route("annual")]
        public ReportModel getAnnualReport(int accountId, DateTime date)
        {
            ReportModel report;
            var expenses = transactionRepository.GetExpenses(accountId, true, date);
            var income = transactionRepository.GetIncome(accountId, true, date);
            var total = income + expenses;
            report = new ReportModel() {
                Month = date.ToString("MMMM", CultureInfo.InvariantCulture),
                Expenses = expenses,
                Income = income,
                Total = total
            };
            return report;
        }

        [HttpGet]
        [Route("monthly")]
        public List<ReportModel> getMonthlyReports(int accountId, DateTime date)
        {
            List<ReportModel> reports = new List<ReportModel>();
            var availableDates = transactionRepository.GetAvailableYearsMonths(accountId);
            foreach ( var availableDate in availableDates )
            {
                if (availableDate.Year == date.Year)
                {
                    var expenses = transactionRepository.GetExpenses(accountId, false, availableDate);
                    var income = transactionRepository.GetIncome(accountId, false, availableDate);
                    var total = income + expenses;
                    reports.Add(new ReportModel()
                    {
                        Month = availableDate.ToString("MMMM", CultureInfo.InvariantCulture),
                        Expenses = expenses,
                        Income = income,
                        Total = total
                    });
                }
            }
            return reports;
        }

        [HttpGet]
        [Route("categoriesreport")]
        public List<CategoryReportModel> GetCategoriesReport(int accountId, bool annual, DateTime date)
        {
            var categoriesReports = transactionRepository.GetReportByCategories(accountId, annual, date);
            return categoriesReports;
        }
    }
}
