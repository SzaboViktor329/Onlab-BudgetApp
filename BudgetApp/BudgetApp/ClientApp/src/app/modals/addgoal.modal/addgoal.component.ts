import { Component, EventEmitter, Output } from '@angular/core';
import { FormControl, FormGroup, NgForm } from '@angular/forms';
import { Categories } from 'src/app/categories/categories';
import { GoalViewModel } from 'src/app/swagger-generated';

declare var bootstrap: any;

@Component({
  selector: 'app-addgoal-modal',
  templateUrl: './addgoal.component.html'
})
export class AddGoalModal {
  public goal : GoalViewModel = {};
  @Output() addGoal = new EventEmitter<GoalViewModel>();

  categories = Object.values(Categories).filter(x => typeof x === "string");
  dateToday=new Date();
  
  public goalForm = new FormGroup({
    category: new FormControl(this.categories[0].toString()),
    annual: new FormControl(false),
    yearMonth: new FormControl(this.dateToday.toISOString().split('T')[0].slice(0,-3)),
    goal: new FormControl()
  });
  
  onSubmit() { 
    this.goal={
      annual: this.goalForm.value.annual as boolean,
      category: this.goalForm.value.category as string,
      goalDate: new Date(this.goalForm.value.yearMonth as string),
      targetAmount: this.goalForm.value.goal
    }
    this.addGoal.emit(this.goal);
    console.log(this.goal);
    this.closeModal();
  }

  closeModal(){
    bootstrap.Modal.getInstance(document.getElementById('addGoalModal')).hide();
    this.goalForm.reset();
    this.goalForm.patchValue({
      category: this.categories[0].toString(),
      annual: false,
      yearMonth: this.dateToday.toISOString().split('T')[0].slice(0,-3)
    });
  }
}
