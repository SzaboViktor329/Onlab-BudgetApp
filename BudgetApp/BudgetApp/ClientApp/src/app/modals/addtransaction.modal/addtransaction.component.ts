import { Component } from '@angular/core';
import { NgForm } from '@angular/forms';

@Component({
  selector: 'app-addtransaction-modal',
  templateUrl: './addtransaction.component.html'
})
export class AddTransactionModal {
    onSubmit(f: NgForm) {
        console.log(f.value)
        }
}
