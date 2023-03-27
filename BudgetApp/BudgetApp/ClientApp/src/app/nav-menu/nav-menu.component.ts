import { Component } from '@angular/core';
import { AuthService } from '../services/auth.service';

@Component({
  selector: 'app-nav-menu',
  templateUrl: './nav-menu.component.html',
  styleUrls: ['./nav-menu.component.css'],
  providers: [AuthService]
})
export class NavMenuComponent {
  isExpanded = false;
  isLoggedIn = false;
  //authService : AuthService = 
  constructor(public authService: AuthService){}

  login(){
    this.isLoggedIn=true;
  }

  logout(){
    this.isLoggedIn=false;
  }

  collapse() {
    this.isExpanded = false;
  }

  toggle() {
    this.isExpanded = !this.isExpanded;
  }
}
