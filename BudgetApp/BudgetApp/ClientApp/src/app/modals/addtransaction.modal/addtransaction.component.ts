import { formatDate } from '@angular/common';
import { Component, EventEmitter, Output } from '@angular/core';
import { FormControl, FormGroup, NgForm } from '@angular/forms';
import { Categories } from 'src/app/categories/categories';
import { TransactionViewModel } from 'src/app/swagger-generated';

declare var bootstrap: any;

@Component({
  selector: 'app-addtransaction-modal',
  templateUrl: './addtransaction.component.html'
})
export class AddTransactionModal {
  public transaction : TransactionViewModel = {};
  @Output() addTransaction = new EventEmitter<TransactionViewModel>();

  categories = Object.values(Categories).filter(x => typeof x === "string");
  dateToday=new Date().toISOString().split('T')[0];
  minDate: string = '';


  public transactionForm = new FormGroup({
    name: new FormControl(),
    category: new FormControl(this.categories[0].toString()),
    monthly: new FormControl(false),
    date: new FormControl(this.dateToday),
    amount: new FormControl()
  });
  
  onSubmit() {
    this.transaction={
      transactionName: this.transactionForm.value.name,
      category: this.transactionForm.value.category as string,
      transactionStatus: this.transactionForm.value.monthly ? "upcoming" : "booked",
      postedDate: new Date(this.transactionForm.value.date as string),
      upcomingDate: new Date(this.transactionForm.value.date as string),
      amount: this.transactionForm.value.amount
    }
    this.addTransaction.emit(this.transaction);
    console.log(this.transaction);
    this.closeModal();
  }

  closeModal(){
    bootstrap.Modal.getInstance(document.getElementById('addTransactionModal')).hide();
    this.transactionForm.reset();
    this.transactionForm.patchValue({
      category: this.categories[0].toString(),
      monthly: false,
      date: this.dateToday
    });
  }

  handleMonthlyChange(checked: boolean) {
    this.minDate = checked ? this.dateToday : '';
    if((checked)&&(this.transactionForm.value.date as string < this.minDate)){
      this.transactionForm.controls.date.setErrors({ 'invalid': true });
    }
  }

  
}
