import { Component } from '@angular/core';
import { FormControl, FormGroup, NgForm, Validators } from '@angular/forms';
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
    private identityService: IdentityService;

    constructor(identityService: IdentityService) {
        this.identityService = identityService;
    }

    onSubmit(f: NgForm) {
        this.registerFormApi = {
            username: f.value.username,
            password: f.value.password,
            email: f.value.email,
            phoneNumber: f.value.phoneNumber,
            firstName: f.value.firstName,
            lastName: f.value.lastName
        }
        
        console.log(this.registerFormApi);
    }
}