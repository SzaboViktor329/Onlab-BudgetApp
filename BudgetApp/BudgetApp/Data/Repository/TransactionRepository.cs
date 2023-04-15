using BudgetApp.Data.Repository.RepoServices;
using BudgetApp.Models;

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
    }
}
