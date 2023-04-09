import { Component } from '@angular/core';

@Component({
  selector: 'app-goals',
  templateUrl: './goals.component.html'
})
export class GoalsComponent {
    public isIncome: boolean = true;
    public percentage: number = 30;
}
