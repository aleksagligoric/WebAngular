import { Component, OnInit } from '@angular/core';
import { User } from '../osoba';
import { AuthHttpService } from '../services/auth.service';
import { NgForm } from '@angular/forms';
import { Router } from '@angular/router';
import { LoginToNavbarService } from '../services/login-to-navbar.service';
import { MainNavbarComponent } from '../main-navbar/main-navbar.component';
import { MainNavbarService } from '../main-navbar/main-navbar.service';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css']
})
export class LoginComponent implements OnInit {

  constructor(private http: AuthHttpService, private router: Router, public service: MainNavbarService) { }

  isLogin: boolean = false;
  loginToNavBar:LoginToNavbarService=new LoginToNavbarService();
  isLoggedIn = false;
  isBadLoginParams: boolean;
  isOtherError: boolean;
  errorDescription: string;
  isError: boolean;

  ngOnInit() {
  }

  login(user: User, form: NgForm){
    this.http.logIn(user, (isLoggedIn, errorStatus, errorDescription) => {
      console.log("sadsa"+isLoggedIn);
      if(isLoggedIn){

          this.isError = false;
          this.isLoggedIn = isLoggedIn;
        //  this.loginToNavBar.login();
          form.resetForm();
         console.log(isLoggedIn);
          this.service.newLogin.next(true);
          this.router.navigate(["/home"]);
         
      }else{
        this.isError = true;
        this.errorDescription = errorDescription;
        alert(errorDescription);
      }

  });
  
      
  }

  
}
