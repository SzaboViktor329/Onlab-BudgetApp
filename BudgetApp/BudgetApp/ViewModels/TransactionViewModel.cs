namespace BudgetApp.ViewModels
{
    public class TransactionViewModel
    {
        public int TransactionId { get; set; }
        public string? TransactionName { get; set; }
        public string? Category { get; set; }
        public string? TransactionStatus { get; set; }
        public DateTime PostedDate { get; set; }
        public DateTime UpcomingDate { get; set; }
        public double Amount { get; set; }
    }
}
