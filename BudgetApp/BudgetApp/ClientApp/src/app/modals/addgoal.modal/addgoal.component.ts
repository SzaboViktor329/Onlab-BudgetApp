import { Component } from '@angular/core';
import { NgForm } from '@angular/forms';

@Component({
  selector: 'app-addgoal-modal',
  templateUrl: './addgoal.component.html'
})
export class AddGoalModal {
    onSubmit(f: NgForm) {
        console.log(f.value)
        }
}
