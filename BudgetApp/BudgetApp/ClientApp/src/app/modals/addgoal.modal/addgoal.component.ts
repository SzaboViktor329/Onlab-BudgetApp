import { Component, EventEmitter, Output } from '@angular/core';
import { FormControl, FormGroup, NgForm } from '@angular/forms';
import { Categories } from 'src/app/categories/categories';
import { AuthService } from 'src/app/services/auth.service';
import { GoalService, GoalViewModel } from 'src/app/swagger-generated';

declare var bootstrap: any;

@Component({
  selector: 'app-addgoal-modal',
  templateUrl: './addgoal.component.html',
  providers: [GoalService]
})
export class AddGoalModal {
  public accountId = this.authService.getSelectedAccountId();
  public goal : GoalViewModel = {};
  @Output() goalAdded = new EventEmitter<GoalViewModel>();

  categories = Object.values(Categories).filter(x => typeof x === "string");
  dateToday=new Date();
  
  public goalForm = new FormGroup({
    category: new FormControl(this.categories[0].toString()),
    annual: new FormControl(false),
    yearMonth: new FormControl(this.dateToday.toISOString().split('T')[0].slice(0,-3)),
    goal: new FormControl()
  });
  
  public goalAlreadyExist: boolean = false;

  constructor(private goalService: GoalService, public authService: AuthService) {}

  onSubmit() { 
    this.goal={
      annual: this.goalForm.value.annual as boolean,
      category: this.goalForm.value.category as string,
      goalDate: new Date(this.goalForm.value.yearMonth as string),
      targetAmount: this.goalForm.value.goal
    }
    this.goalService.apiGoalPost(this.goal, this.accountId).subscribe(response =>{
      console.log(response);
      this.goalAdded.emit(this.goal);
      this.closeModal();
    }, error =>{
      this.goalAlreadyExist=true;
    });
    
    console.log(this.goal);
    //this.closeModal();
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

  onFormInteraction() {
    this.goalAlreadyExist = false;
  }
}
