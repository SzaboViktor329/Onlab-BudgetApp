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
    }
}
