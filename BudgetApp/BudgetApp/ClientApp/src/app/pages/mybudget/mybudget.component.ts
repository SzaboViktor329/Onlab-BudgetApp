import { Component } from '@angular/core';
import { AuthService } from 'src/app/services/auth.service';
import { ThousandSeparatorPipe } from 'src/app/pipes/thousand-separator.pipe';
import { TransactionService, TransactionViewModel } from 'src/app/swagger-generated';

@Component({
  selector: 'app-mybudget',
  templateUrl: './mybudget.component.html',
  providers: [TransactionService]
})
export class MyBudgetComponent {
  public upcomingTransactions: TransactionViewModel[] = [];
  public bookedTransactions: TransactionViewModel[] = [];
  public balance: number = 0;
  public upcoming: number = 0;
  public accountId = this.authService.getSelectedAccountId();
  public account = this.authService.getSelectedAccount();

  public thousandSeparator: ThousandSeparatorPipe = new ThousandSeparatorPipe();

  public currentPage: number = 1;
  public totalPages: number = 1;
  public allTransactions: boolean = false;
  private transactionGetType : string = "thisMonth";

  constructor(private transactionService: TransactionService, public authService: AuthService) {
    console.log(this.currentPage);
    this.getBalance();
    this.getAllTransactions();
  }

  getTotalPages(){
    this.transactionService.apiTransactionGetpagesGet(this.accountId).subscribe(response=>{
      this.totalPages=response;
      if(this.currentPage>this.totalPages){
        this.currentPage=this.totalPages;
      }
      this.getBookedTransactions(this.transactionGetType);
      console.log(this.totalPages);
    });
  }

  addTransaction(transaction: TransactionViewModel) {
    this.transactionService.apiTransactionPost(transaction, this.accountId).subscribe(response => {
      this.getAllTransactions();
      this.getBalance();
      console.log(response);
    });
  }

  getAllTransactions() {
    this.getTotalPages();
    this.getUpcomingTransactions();
    
  }

  getUpcomingTransactions() {
    this.transactionService.apiTransactionUpcomingtransactionsGet(this.accountId).subscribe(response=>{
      this.upcomingTransactions=response;
    });
  }

  getAllBookedTransactions() {
    this.transactionService.apiTransactionBookedtransactionsGet(this.accountId,this.currentPage).subscribe(response => {
      this.bookedTransactions = response;
    });
  }

  getThisMonthsTransactions() {
    this.transactionService.apiTransactionTransactionsinmonthGet(this.accountId,"booked", new Date()).subscribe(response => {
      this.bookedTransactions = response;
    });
  }
  getLastMonthsTransactions() {
    let date = new Date();
    date.setMonth(date.getMonth()-1);
    this.transactionService.apiTransactionTransactionsinmonthGet(this.accountId,"booked", date).subscribe(response => {
      this.bookedTransactions = response;
    });
  }

  removeTransaction(transaction: TransactionViewModel) {
    this.transactionService.apiTransactionDelete(transaction.transactionId).subscribe(response => {
      this.getAllTransactions();
      this.getBalance();
      console.log(response);
    });
  }

  getBalance() {
    this.transactionService.apiTransactionBalanceGet(this.accountId).subscribe(response => {
      this.balance = response;
    });
  }

  getBookedTransactions(type: string) {
    this.transactionGetType=type;
    switch (type) {
      case "thisMonth":
        this.allTransactions = false;
        this.getThisMonthsTransactions();
        break;
      case "lastMonth":
        this.allTransactions = false;
        this.getLastMonthsTransactions();
        break;
      case "all":
        this.allTransactions = true;
        this.getAllBookedTransactions();
        break;
    }
  }

  nextPage() {
    if (this.currentPage < this.totalPages) {
      this.currentPage++;
      this.getAllBookedTransactions();
    }
  }
  previousPage() {
    if (this.currentPage > 1) {
      this.currentPage--;
      this.getAllBookedTransactions();
    }
  }
  getFirstPage() {
    this.currentPage = 1;
    this.getAllBookedTransactions();
  }
  getLastPage() {
    this.currentPage = this.totalPages;
    this.getAllBookedTransactions();
  }

}
