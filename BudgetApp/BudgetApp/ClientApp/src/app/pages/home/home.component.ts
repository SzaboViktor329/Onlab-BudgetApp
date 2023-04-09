import { Component } from '@angular/core';
import { NgForm } from '@angular/forms';
import { AuthService } from 'src/app/services/auth.service';

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html'
})
export class HomeComponent {
  public authService: AuthService;
  constructor(authService: AuthService){
    this.authService = authService;
  }

  parentFunction(input: string){
    console.log(input);
  }

  onSubmit(f: NgForm) {
    console.log(f.value)
    }
    //console.log(this.loginFormApi);
  }

