import { Component } from "@angular/core";
import { FormControl, FormGroup } from "@angular/forms";
import { Router } from "@angular/router";
import { AuthService } from "src/app/services/auth.service";
import { IdentityService, LoginModel } from "src/app/swagger-generated";

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  providers: [IdentityService]
})
export class LoginComponent {
  private loginFormApi: LoginModel = {
    username: "default",
    password: "default"
  };
  public loginForm = new FormGroup({
    username: new FormControl(),
    password: new FormControl()
  });

  public loginFailed: boolean = false;

  constructor(private identityService: IdentityService, public authService: AuthService, private router: Router) {
  }

  onSubmit() {
    this.loginFormApi = {
      username: this.loginForm.controls.username.value,
      password: this.loginForm.controls.password.value
    }
    this.identityService.identityLoginPost(this.loginFormApi).subscribe(response => {
      this.authService.login(response.token);
      this.authService.setUserId();
      this.router.navigate(['/profile']);
    }, error => {
      this.loginFailed = true;
    });
  }

  onFormInteraction() {
    this.loginFailed = false;
  }
}