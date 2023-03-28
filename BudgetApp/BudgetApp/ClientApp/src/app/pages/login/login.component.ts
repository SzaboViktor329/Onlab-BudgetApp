import { Component } from "@angular/core";
import { FormControl, FormGroup, NgForm, Validators } from "@angular/forms";

@Component({
    selector: 'app-login',
    templateUrl: './login.component.html',
  })
  export class LoginComponent {
    loginForm = new FormGroup({
      email: new FormControl('',[Validators.required]),
      password: new FormControl('',[Validators.required])
    });
    onSubmit(f: NgForm) {
      console.log(f.value);
      console.log(f.valid);
    }
    login(){
      
    }
  }