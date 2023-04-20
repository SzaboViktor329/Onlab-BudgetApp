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
        public List<DateTime> getAvailableYearsMonths(int accountId);
        public List<DateTime> getAvailableYears(int accountId);
        public double GetExpenses(int accountId, bool annual, DateTime date);
        public double GetIncome(int accountId, bool annual, DateTime date);
        public List<CategoryReportModel> GetReportByCategories(int accountId, bool annual, DateTime date);
    }
}
