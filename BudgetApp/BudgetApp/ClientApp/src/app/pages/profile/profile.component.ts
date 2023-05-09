import { AfterViewChecked, AfterViewInit, Component, OnInit } from '@angular/core';
import { AuthService } from 'src/app/services/auth.service';
import { AccountService, AccountViewModel, UserService, UserViewModel } from 'src/app/swagger-generated';

@Component({
  selector: 'app-profile',
  templateUrl: './profile.component.html',
  providers: [UserService, AccountService]
})
export class ProfileComponent implements AfterViewChecked {
  public accounts: AccountViewModel[] = [];

  public userViewModel: UserViewModel = {
  };

  constructor(private userService: UserService, private accountService: AccountService, public authService: AuthService) {
    this.getUser();
    this.getAccounts();
    
    //document.getElementById("selectAccount").selectedIndex = "2";

  }
  getUser(){
    this.userService.apiUserGet(this.authService.getUserId()).subscribe(response => {
      this.userViewModel = response
    });
  }
  getAccounts(){
    this.accountService.apiAccountGet(this.authService.getUserId()).subscribe(response => {
      this.accounts = response;
      if(this.authService.getSelectedAccountId()==-1 && this.accounts.length!=0){
        this.authService.setSelectedAccountId(this.accounts[0].accountID as number);
        this.authService.setSelectedAccount(this.accounts[0]);
      }
    });
  }
  addAccount(account : AccountViewModel){
    console.log(account);
    this.accountService.apiAccountCreatePost(account,this.authService.getUserId()).subscribe(response =>{
      console.log(response);
      this.getAccounts();
    });
  }

  ngAfterViewChecked(): void {
    var dropdownList = (document.getElementById("selectAccount")) as HTMLSelectElement;
    if(this.authService.getSelectedAccountId()==-1){
      return;
    }
    var selectedIndex = this.accounts.findIndex(i => i.accountID==this.authService.getSelectedAccountId());
    if(dropdownList!=null){
      dropdownList.selectedIndex = selectedIndex;
    }
    
    //console.log(dropdownList.selectedOptions);
    //console.log(dropdownList.selectedIndex);
  }
  selectChange(){
    var selectedIndex =((document.getElementById("selectAccount")) as HTMLSelectElement).selectedIndex;
    this.authService.setSelectedAccountId(this.accounts[selectedIndex].accountID as number);
    this.authService.setSelectedAccount(this.accounts[selectedIndex]);
    console.log(((document.getElementById("selectAccount")) as HTMLSelectElement).selectedIndex);
  }
}
