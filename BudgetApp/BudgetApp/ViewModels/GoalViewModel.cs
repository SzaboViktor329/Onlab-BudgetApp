namespace BudgetApp.ViewModels
{
    public class GoalViewModel
    {
        public int GoalId { get; set; }
        public bool Annual { get; set; }
        public string? Category { get; set; }
        public DateTime GoalDate { get; set; }
        public double ActualAmount { get; set; }
        public double TargetAmount { get; set; }
    }
}
