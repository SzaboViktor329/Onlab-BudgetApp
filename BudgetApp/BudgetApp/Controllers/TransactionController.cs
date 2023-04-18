using BudgetApp.Data.Repository.RepoServices;
using BudgetApp.Models;
using BudgetApp.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Principal;

namespace BudgetApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TransactionController : ControllerBase
    {
        private readonly ITransactionRepository transactionRepository;
        private readonly IAccountRepository accountRepository;

        public TransactionController(ITransactionRepository transactionRepository, IAccountRepository accountRepository)
        {
            this.transactionRepository = transactionRepository;
            this.accountRepository = accountRepository;
        }

        [HttpPost]
        public IActionResult AddTransaction(TransactionViewModel transactionViewModel, int accountId)
        {
            var account = accountRepository.GetById(accountId);
            if(account== null)
            {
                return BadRequest("Account not exist");
            }
            if (!transactionViewModel.Category.Equals("Income"))
            {
                transactionViewModel.Amount*=-1;
            }
            Transaction transaction = new Transaction() {
                TransactionName= transactionViewModel.TransactionName,
                Category= transactionViewModel.Category,
                TransactionStatus= transactionViewModel.TransactionStatus,
                PostedDate= transactionViewModel.PostedDate,
                UpcomingDate= transactionViewModel.UpcomingDate,
                Amount= transactionViewModel.Amount,
                Account = account
            };
            transactionRepository.Add(transaction);
            return Ok();
        }

        [HttpGet]
        public List<TransactionViewModel> GetTransactions(int accountId, string transactionStatus)
        {
            var transactions = transactionRepository.GetTransactionsOfAccount(accountId, transactionStatus);
            List<TransactionViewModel> transactionViews = new List<TransactionViewModel>();
            foreach (var transaction in transactions)
            {
                transactionViews.Add(new TransactionViewModel()
                {
                    TransactionId = transaction.TransactionId,
                    TransactionName = transaction.TransactionName,
                    Category = transaction.Category,
                    TransactionStatus = transaction.TransactionStatus,
                    PostedDate = transaction.PostedDate,
                    UpcomingDate = transaction.UpcomingDate,
                    Amount = transaction.Amount
                });
            }
            return transactionViews;
        }
        [HttpDelete]
        public IActionResult removeTransaction(int transactionId)
        {
            var transaction = transactionRepository.GetById(transactionId);
            if(transaction == null)
            {
                return BadRequest("Transaction not exist");
            }
            transactionRepository.Delete(transaction);
            return Ok();
        }

        [HttpGet]
        [Route("balance")]
        public double getBalance(int accountId)
        {
            return transactionRepository.getBalance(accountId);
        }
    }
}
