import { Injectable } from "@angular/core";

@Injectable()
export class AuthService{
    login(){
        sessionStorage.setItem("status", "loggedIn");
    }
    logout(){
        sessionStorage.removeItem("status");
    }
    loggedIn() : boolean {
        const status = sessionStorage.getItem("status");
        if(status!=null){
            return true;
        }
        return false;
    }
}