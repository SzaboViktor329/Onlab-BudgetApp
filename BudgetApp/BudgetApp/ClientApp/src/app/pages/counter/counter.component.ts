import { Component } from '@angular/core';
import { AuthService } from 'src/app/services/auth.service';

@Component({
  selector: 'app-counter-component',
  templateUrl: './counter.component.html'
})
export class CounterComponent {
  public currentCount = 0;
  public authService: AuthService;
  constructor(authService: AuthService){
    this.authService = authService;
  }

  public incrementCounter() {
    this.currentCount++;
  }
}
