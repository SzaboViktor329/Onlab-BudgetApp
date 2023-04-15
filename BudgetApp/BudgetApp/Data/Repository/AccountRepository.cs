using BudgetApp.Data.Repository.RepoServices;
using BudgetApp.Models;

namespace BudgetApp.Data.Repository
{
    public class AccountRepository : IAccountRepository
    {
        private readonly ApplicationDBContext context;

        public AccountRepository(ApplicationDBContext context)
        {
            this.context = context;
        }

        public void Add(Account account)
        {
            context.Accounts.Add(account);
            context.SaveChanges();
        }

        public void Delete(Account account)
        {
            context.Accounts.Remove(account);
            context.SaveChanges();
        }

        public List<Account> GetAccountsOfUser(string userId)
        {
            return context.Accounts.Where(q => q.User.Id == userId).ToList();
        }

        public List<Account> GetAll()
        {
            return context.Accounts.ToList();
        }

        public Account GetById(int id)
        {
            return context.Accounts.Find(id);
        }

        public void Update(Account account)
        {
            context.Accounts.Update(account);
            context.SaveChanges();
        }
    }
}
