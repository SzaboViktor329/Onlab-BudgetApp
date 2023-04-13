import { formatDate } from '@angular/common';
import { Component, EventEmitter, Output } from '@angular/core';
import { FormControl, FormGroup, NgForm } from '@angular/forms';
import { Categories } from 'src/app/categories/categories';

declare var bootstrap: any;

@Component({
  selector: 'app-addtransaction-modal',
  templateUrl: './addtransaction.component.html'
})
export class AddTransactionModal {
  public childProp : string = "propfromchild";
  @Output() callBackEvent = new EventEmitter<string>();

  categories = Object.values(Categories).filter(x => typeof x === "string")
  

  public transactionForm = new FormGroup({
    name: new FormControl(),
    category: new FormControl(this.categories[0].toString()),
    monthly: new FormControl(false),
    date: new FormControl(new Date().toISOString().split('T')[0]),
    amount: new FormControl()
  });

  callParent(){
    this.callBackEvent.emit(this.childProp);
  }
  onSubmit() {
    
    this.closeModal();
  }

  closeModal(){
    bootstrap.Modal.getInstance(document.getElementById('addTransactionModal')).hide();
    this.transactionForm.reset();
  }

  
}
