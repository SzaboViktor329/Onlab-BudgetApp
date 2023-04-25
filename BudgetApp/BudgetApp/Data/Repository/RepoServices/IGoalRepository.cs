using BudgetApp.Models;

namespace BudgetApp.Data.Repository.RepoServices
{
    public interface IGoalRepository
    {
        List<Goal> GetAll();
        Goal GetById(int id);
        void Add(Goal goal);
        void Update(Goal goal);
        void Delete(Goal goal);
        List<DateTime> GetAvailableYearsMonths(int accountId);
        List<DateTime> GetAvailableYears(int accountId);
        List<Goal> GetGoalsOfAccount(int accountId, bool annual, DateTime date);
    }
}
