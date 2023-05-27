using BudgetApp.Data.Repository.RepoServices;
using BudgetApp.Models;
using BudgetApp.ViewModels;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace BudgetApp.Data.Repository
{
    public class TransactionRepository : ITransactionRepository
    {
        private readonly ApplicationDBContext context;
        private const int resultsNumber = 5;
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

        public int GetPages(int accountId)
        {
            double rows = context.Transactions.Where(q => q.Account.AccountID == accountId && q.TransactionStatus.Equals("booked")).ToList().Count;
            int result = (int)Math.Ceiling(rows / resultsNumber);
            if(result == 0)
            {
                return 1;
            }
            return result;
        }

        public List<Transaction> GetUpcomingTransactions(int accountId)
        {
            return context.Transactions.Where(q => q.Account.AccountID == accountId && q.TransactionStatus.Equals("upcoming"))
                .OrderByDescending(q => q.PostedDate).ThenByDescending(q => q.TransactionId).ToList();
        }

        public List<Transaction> GetBookedTransactions(int accountId, int page)
        {
            return context.Transactions.Where(q => q.Account.AccountID == accountId && q.TransactionStatus.Equals("booked"))
                .OrderByDescending(q => q.PostedDate).ThenByDescending(q => q.TransactionId).Skip((page-1)*resultsNumber).Take(resultsNumber).ToList();
        }

        public List<Transaction> GetTransactionsInMonth(int accountId, string transactionStatus, DateTime date)
        {
            return context.Transactions
                .Where(q => q.Account.AccountID == accountId && q.TransactionStatus.Equals(transactionStatus) 
                 && q.PostedDate.Year == date.Year && q.PostedDate.Month == date.Month)
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
            return context.Transactions.Where(q => q.Account.AccountID == accountId && q.TransactionStatus.Equals("booked")).Sum(i => i.Amount);
        }

        public double getUpcomingSumInMonth(int accountId, DateTime date)
        {
            return context.Transactions.Where(q => q.Account.AccountID == accountId && q.TransactionStatus.Equals("upcoming") 
            && q.UpcomingDate.Year == date.Year && q.UpcomingDate.Month == date.Month).Sum(i => i.Amount);
        }

        public List<DateTime> GetAvailableYears(int accountId)
        {
            var dates = context.Transactions.Where(q => q.Account.AccountID == accountId && q.TransactionStatus.Equals("booked")).OrderByDescending(q => q.PostedDate).Select(i => i.PostedDate).ToList();
            var result = dates.Select(d => new DateTime(d.Year, 1, 1)).Distinct().ToList();
            return result;
        }

        public List<DateTime> GetAvailableYearsMonths(int accountId)
        {
            var dates = context.Transactions.Where(q => q.Account.AccountID == accountId && q.TransactionStatus.Equals("booked")).OrderByDescending(q => q.PostedDate).Select(i => i.PostedDate).ToList();
            var result = dates.Select(d => new DateTime(d.Year, d.Month, 1)).Distinct().ToList();
            return result;
        }

        public double GetExpenses(int accountId, bool annual, DateTime date)
        {
            double result =0;
            if (annual)
            {
                result = context.Transactions
                    .Where(q => q.Account.AccountID == accountId && q.TransactionStatus.Equals("booked")
                    && q.PostedDate.Year==date.Year && q.Amount<0).Sum(i => i.Amount);
            }
            else
            {
                result = context.Transactions
                    .Where(q => q.Account.AccountID == accountId && q.PostedDate.Year == date.Year && q.TransactionStatus.Equals("booked")
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
                    .Where(q => q.Account.AccountID == accountId && q.TransactionStatus.Equals("booked")
                    && q.PostedDate.Year == date.Year && q.Amount >= 0).Sum(i => i.Amount);
            }
            else
            {
                result = context.Transactions
                    .Where(q => q.Account.AccountID == accountId && q.PostedDate.Year == date.Year && q.TransactionStatus.Equals("booked")
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
                .Where(q => q.Account.AccountID == accountId && q.TransactionStatus.Equals("booked")
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
                .Where(q => q.Account.AccountID == accountId && q.TransactionStatus.Equals("booked")
                    && q.PostedDate.Year == date.Year
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
                .Where(q => q.Account.AccountID == accountId && q.TransactionStatus.Equals("booked")
                    && q.PostedDate.Year == date.Year && category.Equals(q.Category)).Sum(c => c.Amount);
            }
            else
            {
                result = context.Transactions
                .Where(q => q.Account.AccountID == accountId && q.TransactionStatus.Equals("booked")
                    && q.PostedDate.Year == date.Year
                    && q.PostedDate.Month == date.Month && category.Equals(q.Category)).Sum(c => c.Amount);
            }
            return result;
        }

        public bool TransactionOfUser(string userId, int transactiontId)
        {
            return context.Transactions.Any(q=> q.Account.User.Id == userId && q.TransactionId == transactiontId);
        }
    }
}
