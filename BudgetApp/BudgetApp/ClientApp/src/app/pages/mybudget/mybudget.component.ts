import { Component } from '@angular/core';
import { AuthService } from 'src/app/services/auth.service';
import { TransactionService, TransactionViewModel } from 'src/app/swagger-generated';

@Component({
  selector: 'app-mybudget',
  templateUrl: './mybudget.component.html',
  providers: [TransactionService]
})
export class MyBudgetComponent {
  public upcomingTransactions : TransactionViewModel[] = [];
  public bookedTransactions : TransactionViewModel[] = [];
  public balance : number = 0;
  public accountId = this.authService.getSelectedAccountId();
  public account = this.authService.getSelectedAccount();

  constructor(private transactionService : TransactionService, public authService: AuthService){
    this.getBalance();
    this.getAllTransactions();
  }

  addTransaction(transaction : TransactionViewModel){
    this.transactionService.apiTransactionPost(transaction,this.accountId).subscribe(response=>{
      this.getAllTransactions();
      this.getBalance();
      console.log(response);
    });
  }

  getAllTransactions(){
    this.getUpcomingTransactions();
    this.getBookedTransactions();
  }

  getUpcomingTransactions(){
    this.transactionService.apiTransactionGet(this.accountId,"upcoming").subscribe(response=>{
      this.upcomingTransactions=response;
    });
  }

  getBookedTransactions(){
    this.transactionService.apiTransactionGet(this.accountId,"booked").subscribe(response=>{
      this.bookedTransactions=response;
    });
  }

  removeTransaction(transaction: TransactionViewModel){
    this.transactionService.apiTransactionDelete(transaction.transactionId).subscribe(response=>{
      this.getAllTransactions();
      this.getBalance();
      console.log(response);
    });
  }

  getBalance(){
    this.transactionService.apiTransactionBalanceGet(this.accountId).subscribe(response=>{
      this.balance=response;
    });
  }
}
