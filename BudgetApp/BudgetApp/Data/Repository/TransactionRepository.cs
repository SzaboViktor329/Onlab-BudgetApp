using BudgetApp.Data.Repository.RepoServices;
using BudgetApp.Models;
using BudgetApp.ViewModels;

namespace BudgetApp.Data.Repository
{
    public class TransactionRepository : ITransactionRepository
    {
        private readonly ApplicationDBContext context;

        public TransactionRepository(ApplicationDBContext context)
        {
            this.context = context;
        }

        public void Add(Transaction transaction)
        {
            context.Transactions.Add(transaction);
            context.SaveChanges();
        }

        public void Delete(Transaction transaction)
        {
            context.Transactions.Remove(transaction);
            context.SaveChanges();
        }

        public List<Transaction> GetAll()
        {
            return context.Transactions.ToList();
        }

        public List<Transaction> GetTransactionsOfAccount(int accountId, string transactionStatus)
        {
            return context.Transactions.Where(q => q.Account.AccountID == accountId && q.TransactionStatus.Equals(transactionStatus))
                .OrderByDescending(q => q.PostedDate).ThenByDescending(q => q.TransactionId).ToList();
        }

        public Transaction GetById(int id)
        {
            return context.Transactions.Find(id);
        }

        public void Update(Transaction transaction)
        {
            context.Transactions.Update(transaction);
            context.SaveChanges();
        }

        public double getBalance(int accountId)
        {
            return context.Transactions.Where(q => q.Account.AccountID == accountId).Sum(i => i.Amount);
        }

        public List<DateTime> GetAvailableYears(int accountId)
        {
            var dates = context.Transactions.Where(q => q.Account.AccountID == accountId).OrderByDescending(q => q.PostedDate).Select(i => i.PostedDate).ToList();
            var result = dates.Select(d => new DateTime(d.Year, 1, 1)).Distinct().ToList();
            return result;
        }

        public List<DateTime> GetAvailableYearsMonths(int accountId)
        {
            var dates = context.Transactions.Where(q => q.Account.AccountID == accountId).OrderByDescending(q => q.PostedDate).Select(i => i.PostedDate).ToList();
            var result = dates.Select(d => new DateTime(d.Year, d.Month, 1)).Distinct().ToList();
            return result;
        }

        public double GetExpenses(int accountId, bool annual, DateTime date)
        {
            double result =0;
            if (annual)
            {
                result = context.Transactions
                    .Where(q => q.Account.AccountID == accountId
                    && q.PostedDate.Year==date.Year && q.Amount<0).Sum(i => i.Amount);
            }
            else
            {
                result = context.Transactions
                    .Where(q => q.Account.AccountID == accountId && q.PostedDate.Year == date.Year
                    && q.PostedDate.Month==date.Month && q.Amount < 0).Sum(i => i.Amount);
            }
            return result;
        }

        public double GetIncome(int accountId, bool annual, DateTime date)
        {
            double result = 0;
            if (annual)
            {
                result = context.Transactions
                    .Where(q => q.Account.AccountID == accountId
                    && q.PostedDate.Year == date.Year && q.Amount >= 0).Sum(i => i.Amount);
            }
            else
            {
                result = context.Transactions
                    .Where(q => q.Account.AccountID == accountId && q.PostedDate.Year == date.Year
                    && q.PostedDate.Month == date.Month && q.Amount >= 0).Sum(i => i.Amount);
            }
            return result;
        }

        public List<CategoryReportModel> GetReportByCategories(int accountId, bool annual, DateTime date)
        {
            List<CategoryReportModel> results;
            if (annual)
            {
                results = context.Transactions
                .Where(q => q.Account.AccountID == accountId
                    && q.PostedDate.Year == date.Year)
                .GroupBy(c => c.Category)
                .Select(g => new CategoryReportModel()
                {
                    Category = g.Key,
                    Amount = Math.Abs(g.Sum(s => s.Amount))
                }).ToList();
            }
            else
            {
                results = context.Transactions
                .Where(q => q.Account.AccountID == accountId && q.PostedDate.Year == date.Year
                    && q.PostedDate.Month == date.Month)
                .GroupBy(c => c.Category)
                .Select(g => new CategoryReportModel()
                {
                    Category = g.Key,
                    Amount = Math.Abs(g.Sum(s => s.Amount))
                }).ToList();
            }
            return results;
        }

        public double GetSumByCategory(int accountId, bool annual, DateTime date, string category)
        {
            double result;
            if (annual)
            {
                result = context.Transactions
                .Where(q => q.Account.AccountID == accountId
                    && q.PostedDate.Year == date.Year && category.Equals(q.Category)).Sum(c => c.Amount);
            }
            else
            {
                result = context.Transactions
                .Where(q => q.Account.AccountID == accountId && q.PostedDate.Year == date.Year
                    && q.PostedDate.Month == date.Month && category.Equals(q.Category)).Sum(c => c.Amount);
            }
            return result;
        }
    }
}
