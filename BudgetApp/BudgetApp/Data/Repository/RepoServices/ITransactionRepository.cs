using BudgetApp.Models;

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
    }
}
