import { Component } from '@angular/core';
import { FormControl, FormGroup, NgForm, Validators } from '@angular/forms';
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

    constructor(private identityService: IdentityService, private router: Router) {
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
        this.identityService.identityRegisterPost(this.registerFormApi).subscribe(response => {
            console.log(response.result);
            this.router.navigate(['/login']);
        }, error => console.log("nem jo"));
        
        console.log(this.registerFormApi);
    }
}