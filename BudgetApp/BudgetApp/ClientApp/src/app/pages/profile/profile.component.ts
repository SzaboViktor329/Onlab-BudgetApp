import { Component } from '@angular/core';

@Component({
  selector: 'app-profile',
  templateUrl: './profile.component.html'
})
export class ProfileComponent {
    public accounts: string[] = [
      "First acc 1111-2222",
      "Second acc 1111-3333",
      "Third acc 1111-4444"
    ]

    constructor(){
      console.log(this.accounts);
    }
}
