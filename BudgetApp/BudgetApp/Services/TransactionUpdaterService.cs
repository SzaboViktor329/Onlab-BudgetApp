using BudgetApp.Data.Repository.RepoServices;

namespace BudgetApp.Services
{
    public class TransactionUpdaterService : BackgroundService
    {
        private readonly IServiceProvider serviceProvider;

        public TransactionUpdaterService(IServiceProvider serviceProvider)
        {
            this.serviceProvider = serviceProvider;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            using(var scope = serviceProvider.CreateScope())
            {
                ITransactionRepository transactionRepository = scope.ServiceProvider.GetRequiredService<ITransactionRepository>();
                while (!stoppingToken.IsCancellationRequested)
                {
                    await UpdateTransactions(transactionRepository);
                    await Task.Delay(TimeSpan.FromHours(2), stoppingToken);
                }
            }
        }

        private Task UpdateTransactions(ITransactionRepository transactionRepository)
        {
            transactionRepository.UpdateUpcomingTransactions();
            System.Diagnostics.Debug.WriteLine("Upcoming transactions updated.");
            return Task.CompletedTask;
        }
    }
}
