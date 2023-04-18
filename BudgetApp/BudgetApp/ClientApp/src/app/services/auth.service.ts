import { Injectable } from "@angular/core";
import { JwtHelperService } from "@auth0/angular-jwt";
import { AccountViewModel } from "../swagger-generated";

@Injectable({
    providedIn: "root"
})
export class AuthService{
    public isLoggedIn : boolean = true;
    private userId : string ="default";
    private selectedAccountId : number =-1;
    private selectedAccount : AccountViewModel ={};

    helper = new JwtHelperService();

    constructor(){
        this.setUserId();
        const selectedAccount = sessionStorage.getItem("selectedAccount");
        if(selectedAccount!=null){
            this.selectedAccountId = +selectedAccount;
        }
    }

    public login(token : string){
        this.isLoggedIn=true;
        sessionStorage.setItem("jwt", token);
    }
    public logout(){
        sessionStorage.removeItem("jwt");
        sessionStorage.removeItem("selectedAccount");
        this.selectedAccountId=-1;
        this.userId="default";
        this.isLoggedIn=false;
    }
    loggedIn() : boolean {
        const status = sessionStorage.getItem("status");
        if(status!=null){
            return true;
        }
        return false;
    }
    getUserId(){
        return this.userId;
    }
    setUserId(){
        let token = sessionStorage.getItem("jwt");
        if(token!=null){
            let decodedToken = this.helper.decodeToken(token);
            this.userId = decodedToken.user_id;
        }     
    }
    getSelectedAccountId(){
        return this.selectedAccountId;
    }
    setSelectedAccountId(selectedAccountId : number){
        this.selectedAccountId=selectedAccountId;
        sessionStorage.setItem("selectedAccount",selectedAccountId.toString());
    }
    getSelectedAccount(){
        return this.selectedAccount;
    }
    setSelectedAccount(account : AccountViewModel){
        this.selectedAccount=account;
    }
}