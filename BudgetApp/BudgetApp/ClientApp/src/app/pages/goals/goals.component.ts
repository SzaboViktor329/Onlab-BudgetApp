import { Component } from '@angular/core';
import { ThousandSeparatorPipe } from 'src/app/pipes/thousand-separator.pipe';
import { AuthService } from 'src/app/services/auth.service';
import { GoalService, GoalViewModel } from 'src/app/swagger-generated';

@Component({
  selector: 'app-goals',
  templateUrl: './goals.component.html',
  providers: [GoalService]
})
export class GoalsComponent {
  public accountId = this.authService.getSelectedAccountId();
  public annualGoals : GoalViewModel[] = [];
  public monthlyGoals : GoalViewModel[] = [];
  public availableYearsMonths : Date[] = [];
  public availableYears : Date[] = [];
  public isIncome: boolean = true;
  public percentage: number = 30;

  public thousandSeparator : ThousandSeparatorPipe = new ThousandSeparatorPipe();

  constructor(private goalService: GoalService, public authService: AuthService) {
    this.getAvailableYears();
    this.getAvailableYearsMonths();
  }

  getAvailableYears(){
    this.availableYears = [];
    this.goalService.apiGoalAvailableyearsGet(this.accountId).subscribe(response=>{
      for(var dateString of response){
        this.availableYears.push(new Date(dateString));
      }
      this.getAnnualGoals(this.availableYears[0]);
    });
  }
  getAvailableYearsMonths(){
    this.availableYearsMonths = [];
    this.goalService.apiGoalAvailabledatesGet(this.accountId).subscribe(response=>{
      for(var dateString of response){
        this.availableYearsMonths.push(new Date(dateString));
      }
      this.getMonthlyGoals(this.availableYearsMonths[0]);
    });
  }

  annualGoalSelectionChanged(dateString : string){
    this.getAnnualGoals(new Date(dateString));
  }

  monthlyGoalSelectionChanged(dateString : string){
    this.getMonthlyGoals(new Date(dateString));
  }

  getAnnualGoals(date : Date){
    this.goalService.apiGoalGet(this.accountId,true,date).subscribe(response=>{
      this.annualGoals=response;
    })
  }

  getMonthlyGoals(date : Date){
    this.goalService.apiGoalGet(this.accountId,false,date).subscribe(response=>{
      this.monthlyGoals=response;
    })
  }

  goalAdded(goal: GoalViewModel) {
    this.getAvailableYears();
    this.getAvailableYearsMonths();
  }
  
  removeGoal(goal: GoalViewModel){
    this.goalService.apiGoalDelete(goal.goalId).subscribe(response=>{
      this.getAvailableYears();
      this.getAvailableYearsMonths();
      console.log(response);
    });
  }
}
