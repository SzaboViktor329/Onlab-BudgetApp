import { Component, EventEmitter, Output } from '@angular/core';
import { NgForm } from '@angular/forms';

@Component({
  selector: 'app-addaccount-modal',
  templateUrl: './addaccount.component.html'
})
export class AddAccountModal {
  public childProp : string = "propfromchild";
  @Output() callBackEvent = new EventEmitter<string>();
  callParent(){
    this.callBackEvent.emit(this.childProp);
  }
  onSubmit(f: NgForm) {
    console.log(f.value)
  }
}
