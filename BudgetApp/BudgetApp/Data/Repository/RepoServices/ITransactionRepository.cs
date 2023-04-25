using BudgetApp.Models;
using BudgetApp.ViewModels;

namespace BudgetApp.Data.Repository.RepoServices
{
    public interface ITransactionRepository
    {
        List<Transaction> GetAll();
        Transaction GetById(int id);
        void Add(Transaction transaction);
        void Update(Transaction transaction);
        void Delete(Transaction transaction);
        List<Transaction> GetTransactionsOfAccount(int accountId, string transactionStatus);
        double getBalance(int accountId);
        List<DateTime> GetAvailableYearsMonths(int accountId);
        List<DateTime> GetAvailableYears(int accountId);
        double GetExpenses(int accountId, bool annual, DateTime date);
        double GetIncome(int accountId, bool annual, DateTime date);
        double GetSumByCategory(int accountId, bool annual, DateTime date, string category);
        List<CategoryReportModel> GetReportByCategories(int accountId, bool annual, DateTime date);
    }
}
