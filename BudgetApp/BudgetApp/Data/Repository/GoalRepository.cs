using BudgetApp.Data.Repository.RepoServices;
using BudgetApp.Models;

namespace BudgetApp.Data.Repository
{
    public class GoalRepository : IGoalRepository
    {
        private readonly ApplicationDBContext context;

        public GoalRepository(ApplicationDBContext context)
        {
            this.context = context;
        }

        public void Add(Goal goal)
        {
            context.Goals.Add(goal);
            context.SaveChanges();
        }

        public void Delete(Goal goal)
        {
            context.Goals.Remove(goal);
            context.SaveChanges();
        }

        public List<Goal> GetAll()
        {
            return context.Goals.ToList();
        }

        public List<DateTime> GetAvailableYears(int accountId)
        {
            var dates = context.Goals.Where(q=>q.Account.AccountID == accountId && q.Annual==true).OrderByDescending(q=>q.GoalDate).Select(q=>q.GoalDate).ToList();
            var result = dates.Select(d => new DateTime(d.Year, 1, 1)).Distinct().ToList();
            return result;
        }

        public List<DateTime> GetAvailableYearsMonths(int accountId)
        {
            var dates = context.Goals.Where(q => q.Account.AccountID == accountId && q.Annual==false).OrderByDescending(q => q.GoalDate).Select(q => q.GoalDate).ToList();
            var result = dates.Select(d => new DateTime(d.Year, d.Month, 1)).Distinct().ToList();
            return result;
        }

        public Goal GetById(int id)
        {
            return context.Goals.Find(id);
        }

        public List<Goal> GetGoalsOfAccount(int accountId, bool annual, DateTime date)
        {
            if (annual)
            {
                return context.Goals.Where(q => q.Account.AccountID == accountId && q.Annual == annual 
                && q.GoalDate.Year == date.Year).ToList();
            }
            return context.Goals.Where(q => q.Account.AccountID == accountId && q.Annual == annual
            && q.GoalDate.Year == date.Year && q.GoalDate.Month == date.Month).ToList();
        }

        public bool GoalOfUser(string userId, int goalId)
        {
            return context.Goals.Any(q => q.Account.User.Id == userId && q.GoalId == goalId);
        }

        public void Update(Goal goal)
        {
            context.Goals.Update(goal);
            context.SaveChanges();
        }
    }
}
