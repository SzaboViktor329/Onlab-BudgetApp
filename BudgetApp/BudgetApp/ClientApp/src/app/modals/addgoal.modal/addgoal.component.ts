import { Component, EventEmitter, Output } from '@angular/core';
import { FormControl, FormGroup, NgForm } from '@angular/forms';

declare var bootstrap: any;

@Component({
  selector: 'app-addgoal-modal',
  templateUrl: './addgoal.component.html'
})
export class AddGoalModal {
  public childProp : string = "propfromchild";
  @Output() callBackEvent = new EventEmitter<string>();
  
  public accountForm = new FormGroup({
    accountName: new FormControl(),
    accountNumber: new FormControl()
  });

  callParent(){
    this.callBackEvent.emit(this.childProp);
  }
  onSubmit() {
    
    this.closeModal();
  }

  closeModal(){
    bootstrap.Modal.getInstance(document.getElementById('addAccountModal')).hide();
    this.accountForm.reset();
  }
}
