using BudgetApp.Models;

namespace BudgetApp.Data.Repository.RepoServices
{
    public interface IAccountRepository
    {
        List<Account> GetAll();
        Account GetById(int id);
        void Add(Account account);
        void Update(Account account);
        void Delete(Account account);
        bool AccountOfUser(string userId, int accountId);
        List<Account> GetAccountsOfUser(string userId);
    }
}
