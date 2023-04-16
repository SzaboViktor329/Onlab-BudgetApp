import { Component, EventEmitter, Output } from '@angular/core';
import { FormControl, FormGroup, NgForm } from '@angular/forms';
import { AccountViewModel } from 'src/app/swagger-generated';

declare var bootstrap: any;

@Component({
  selector: 'app-addaccount-modal',
  templateUrl: './addaccount.component.html'
})
export class AddAccountModal {
  public account : AccountViewModel = {};
  @Output() addAccount = new EventEmitter<AccountViewModel>();
  
  public accountForm = new FormGroup({
    accountName: new FormControl(),
    accountNumber: new FormControl()
  });

  onSubmit() {
    this.account={
      accountName: this.accountForm.value.accountName,
      accountNumber: this.accountForm.value.accountNumber
    }
    this.addAccount.emit(this.account);
    this.closeModal();
  }

  closeModal(){
    bootstrap.Modal.getInstance(document.getElementById('addAccountModal')).hide();
    this.accountForm.reset();
  }
}
