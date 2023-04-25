import { Component, EventEmitter, Output } from '@angular/core';
import { FormControl, FormGroup, NgForm } from '@angular/forms';
import { Categories } from 'src/app/categories/categories';

declare var bootstrap: any;

@Component({
  selector: 'app-addgoal-modal',
  templateUrl: './addgoal.component.html'
})
export class AddGoalModal {
  public childProp : string = "propfromchild";
  @Output() callBackEvent = new EventEmitter<string>();

  categories = Object.values(Categories).filter(x => typeof x === "string");
  dateToday=new Date();
  
  public goalForm = new FormGroup({
    category: new FormControl(this.categories[0].toString()),
    annual: new FormControl(false),
    yearMonth: new FormControl(this.dateToday.toISOString().split('T')[0].slice(0,-3)),
    goal: new FormControl()
  });

  callParent(){
    this.callBackEvent.emit(this.childProp);
  }
  onSubmit() { 
    console.log(this.goalForm);
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
