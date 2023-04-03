namespace BudgetApp.Models
{
    public class Account
    {
        public int AccountID { get; set; }
        public string? AccountNumber { get; set; }
        public string? AccountName { get; set; }
        public virtual User? User { get; set; }
        public virtual ICollection<Transaction>? Transactions { get; set; }
        public virtual ICollection<Goal>? Goals { get; set; }
    }
}
