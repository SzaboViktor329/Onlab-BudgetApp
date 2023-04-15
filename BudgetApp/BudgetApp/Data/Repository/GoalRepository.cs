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

        public Goal GetById(int id)
        {
            return context.Goals.Find(id);
        }

        public void Update(Goal goal)
        {
            context.Goals.Update(goal);
            context.SaveChanges();
        }
    }
}
