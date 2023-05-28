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
            if (!accountRepository.AccountOfUser(HttpContext.User.Claims.FirstOrDefault(c => c.Type == "user_id")?.Value.ToString() ?? string.Empty, accountId))
            {
                return BadRequest("Not the user's account");
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
        [Route("upcomingtransactions")]
        public List<TransactionViewModel> GetUpcomingTransactions(int accountId)
        {
            if (!accountRepository.AccountOfUser(HttpContext.User.Claims.FirstOrDefault(c => c.Type == "user_id")?.Value.ToString() ?? string.Empty, accountId))
            {
                return new List<TransactionViewModel>();
            }
            var transactions = transactionRepository.GetUpcomingTransactions(accountId);
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
        [HttpGet]
        [Route("bookedtransactions")]
        public List<TransactionViewModel> GetBookedTransactions(int accountId, int page)
        {
            if (!accountRepository.AccountOfUser(HttpContext.User.Claims.FirstOrDefault(c => c.Type == "user_id")?.Value.ToString() ?? string.Empty, accountId))
            {
                return new List<TransactionViewModel>();
            }
            var transactions = transactionRepository.GetBookedTransactions(accountId,page);
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

        [HttpGet]
        [Route("getpages")]
        public int GetPages(int accountId)
        {
            if(!accountRepository.AccountOfUser(HttpContext.User.Claims.FirstOrDefault(c => c.Type == "user_id")?.Value.ToString() ?? string.Empty, accountId))
            {
                return 1;
            }
            return transactionRepository.GetPages(accountId);
        }


        [HttpGet]
        [Route("transactionsinmonth")]
        public List<TransactionViewModel> GetTransactionsInMonth(int accountId, string transactionStatus, DateTime date)
        {
            if (!accountRepository.AccountOfUser(HttpContext.User.Claims.FirstOrDefault(c => c.Type == "user_id")?.Value.ToString() ?? string.Empty, accountId))
            {
                return new List<TransactionViewModel>();
            }
            var transactions = transactionRepository.GetTransactionsInMonth(accountId, transactionStatus, date);
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
            if(!transactionRepository.TransactionOfUser(HttpContext.User.Claims.FirstOrDefault(c => c.Type == "user_id")?.Value.ToString() ?? string.Empty, transactionId))
            {
                return BadRequest("Not the user's transaction");
            }
            transactionRepository.Delete(transaction);
            return Ok();
        }

        [HttpGet]
        [Route("balance")]
        public double getBalance(int accountId)
        {
            if (!accountRepository.AccountOfUser(HttpContext.User.Claims.FirstOrDefault(c => c.Type == "user_id")?.Value.ToString() ?? string.Empty, accountId))
            {
                return -1;
            }
            return transactionRepository.getBalance(accountId);
        }

        [HttpGet]
        [Route("upcominginmonth")]
        public double getUpcomingSumInMonth(int accountId, DateTime date)
        {
            if (!accountRepository.AccountOfUser(HttpContext.User.Claims.FirstOrDefault(c => c.Type == "user_id")?.Value.ToString() ?? string.Empty, accountId))
            {
                return -1;
            }
            return transactionRepository.getUpcomingSumInMonth(accountId, date);
        }
    }
}
