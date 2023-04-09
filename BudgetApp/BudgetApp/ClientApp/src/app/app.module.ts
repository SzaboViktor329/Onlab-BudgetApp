import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { HttpClientModule } from '@angular/common/http';
import { RouterModule } from '@angular/router';
import { JwtModule } from '@auth0/angular-jwt';

import { AppComponent } from './app.component';
import { NavMenuComponent } from './nav-menu/nav-menu.component';
import { HomeComponent } from './pages/home/home.component';
import { CounterComponent } from './pages/counter/counter.component';
import { FetchDataComponent } from './pages/fetch-data/fetch-data.component';
import { LoginComponent } from './pages/login/login.component';
import { RegisterComponent } from './pages/register/register.component';
import { ProfileComponent } from './pages/profile/profile.component';
import { AuthService } from './services/auth.service';
import { MyBudgetComponent } from './pages/mybudget/mybudget.component';
import { ReportsComponent } from './pages/reports/reports.component';
import { GoalsComponent } from './pages/goals/goals.component';

export function tokenGetter(){
  return sessionStorage.getItem("jwt");
}

@NgModule({
  declarations: [
    AppComponent,
    NavMenuComponent,
    HomeComponent,
    CounterComponent,
    FetchDataComponent,
    LoginComponent,
    RegisterComponent,
    ProfileComponent,
    MyBudgetComponent,
    ReportsComponent,
    GoalsComponent
  ],
  imports: [
    BrowserModule.withServerTransition({ appId: 'ng-cli-universal' }),
    HttpClientModule,
    FormsModule,
    RouterModule.forRoot([
      { path: '', component: HomeComponent, pathMatch: 'full' },
      { path: 'counter', component: CounterComponent },
      { path: 'fetch-data', component: FetchDataComponent },
      { path: 'login', component: LoginComponent },
      { path: 'register', component: RegisterComponent },
      { path: 'profile', component: ProfileComponent },
      { path: 'mybudget', component: MyBudgetComponent },
      { path: 'reports', component: ReportsComponent },
      { path: 'goals', component: GoalsComponent },
    ]),
    JwtModule.forRoot({
      config: {
        tokenGetter: tokenGetter
      }
    })
  ],
  providers: [AuthService],
  bootstrap: [AppComponent]
})
export class AppModule { }
