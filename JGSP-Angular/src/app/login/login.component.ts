import { Component, OnInit } from '@angular/core';
import { User } from '../osoba';
import { AuthHttpService } from '../services/auth.service';
import { NgForm } from '@angular/forms';
import { Router } from '@angular/router';
import { LoginToNavbarService } from '../services/login-to-navbar.service';
import { MainNavbarComponent } from '../main-navbar/main-navbar.component';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css']
})
export class LoginComponent implements OnInit {

  constructor(private http: AuthHttpService, private router: Router) { }

  isLogin: boolean = false;
  loginToNavBar:LoginToNavbarService=new LoginToNavbarService();

  ngOnInit() {
  }

  login(user: User, form: NgForm){
    let l = this.http.logIn(user.username, user.password);
    this.loginToNavBar.login();
    form.resetForm();
   
    this.router.navigate(["/home"]);
  
      
  }

  
}
