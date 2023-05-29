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
        List<Transaction> GetUpcomingTransactions(int accountId);
        List<Transaction> GetBookedTransactions(int accountId, int page);
        List<Transaction> GetTransactionsInMonth(int accountId, string transactionStatus, DateTime date);
        void UpdateUpcomingTransactions();
        bool TransactionOfUser(string userId, int transactiontId);
        int GetPages(int accountId);
        double getBalance(int accountId);
        double getUpcomingSumInMonth(int accountId, DateTime date);
        List<DateTime> GetAvailableYearsMonths(int accountId);
        List<DateTime> GetAvailableYears(int accountId);
        double GetExpenses(int accountId, bool annual, DateTime date);
        double GetIncome(int accountId, bool annual, DateTime date);
        double GetSumByCategory(int accountId, bool annual, DateTime date, string category);
        List<CategoryReportModel> GetReportByCategories(int accountId, bool annual, DateTime date);
    }
}
