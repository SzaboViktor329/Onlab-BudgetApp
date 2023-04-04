import { Injectable } from "@angular/core";

@Injectable({
    providedIn: "root"
})
export class AuthService{
    public isLoggedIn : boolean = false;

    public login(token : string){
        this.isLoggedIn=true;
        sessionStorage.setItem("jwt", token);
    }
    public logout(){
        sessionStorage.removeItem("jwt");
        this.isLoggedIn=false;
    }
    loggedIn() : boolean {
        const status = sessionStorage.getItem("status");
        if(status!=null){
            return true;
        }
        return false;
    }
}