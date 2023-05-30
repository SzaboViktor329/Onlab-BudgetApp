import { Component } from '@angular/core';
import { FormControl, FormGroup } from '@angular/forms';
import { Router } from '@angular/router';
import { IdentityService, RegisterModel } from 'src/app/swagger-generated';

@Component({
    selector: 'app-register',
    templateUrl: './register.component.html',
    providers: [IdentityService]
})
export class RegisterComponent {
    private registerFormApi: RegisterModel = {
        username: "default",
        password: "default",
        email: 'default',
        phoneNumber: 'default',
        firstName: 'default',
        lastName: 'default'
    };
    public registerForm = new FormGroup({
        username: new FormControl(),
        password: new FormControl(),
        confirmPassword: new FormControl(),
        email: new FormControl(),
        phoneNumber: new FormControl(),
        firstName: new FormControl(),
        lastName: new FormControl()
    });

    public registerFailed: boolean = false;

    constructor(private identityService: IdentityService, private router: Router) {
    }

    onSubmit() {
        this.registerFormApi = {
            username: this.registerForm.controls.username.value,
            password: this.registerForm.controls.password.value,
            email: this.registerForm.controls.email.value,
            phoneNumber: this.registerForm.controls.phoneNumber.value,
            firstName: this.registerForm.controls.firstName.value,
            lastName: this.registerForm.controls.lastName.value
        }
        this.identityService.identityRegisterPost(this.registerFormApi).subscribe(response => {
            this.router.navigate(['/login']);
        }, error => {
            this.registerFailed = true;
        });
    }

    onFormInteraction() {
        this.registerFailed = false;
    }

    handleConfirmPasswordChange() {
        if (this.registerForm.controls.password.value !== this.registerForm.controls.confirmPassword.value) {
            this.registerForm.controls.confirmPassword.setErrors({ 'invalid': true });
        }
    }
}