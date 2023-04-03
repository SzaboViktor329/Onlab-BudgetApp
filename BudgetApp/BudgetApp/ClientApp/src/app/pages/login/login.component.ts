import { Component } from "@angular/core";
import { FormControl, FormGroup, NgForm, Validators } from "@angular/forms";
import { IdentityService, LoginModel } from "src/app/swagger-generated";

@Component({
    selector: 'app-login',
    templateUrl: './login.component.html',
    providers: [IdentityService]
  })
  export class LoginComponent {
    private loginFormApi : LoginModel = {
      username: "default",
      password: "default"
    };
    private loginForm = new FormGroup({
      username: new FormControl('',[Validators.required]),
      password: new FormControl('',[Validators.required])
    });
    private identityService : IdentityService;
    
    constructor(identityService : IdentityService){
      this.identityService=identityService;
    }

    onSubmit(f: NgForm) {
      this.loginFormApi={
        username: f.value.username,
        password: f.value.password
      }
      this.identityService.identityLoginPost(this.loginFormApi).subscribe(response =>{
        sessionStorage.setItem("jwt", response.token);
        console.log(response.token);
      }, error => console.log("nem jo"));
      //console.log(this.loginFormApi);
    }
    login(){
      
    }
  }