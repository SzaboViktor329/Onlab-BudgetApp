namespace BudgetApp.Models
{
    public class Goal
    {
        public int GoalId { get; set; }
        public bool Annual { get; set; }
        public string? Category { get; set; }
        public DateTime GoalDate { get; set; }
        public double TargetAmount { get; set; }
        public virtual Account? Account { get; set; }
    }
}
