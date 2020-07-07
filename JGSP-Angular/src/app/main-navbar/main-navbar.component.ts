import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { Observable } from 'rxjs';
import { MainNavbarService } from './main-navbar.service';
import { LoginToNavbarService } from '../services/login-to-navbar.service';

@Component({
  selector: 'app-main-navbar',
  templateUrl: './main-navbar.component.html',
  styleUrls: ['./main-navbar.component.css']
})
export class MainNavbarComponent implements OnInit {

  userRole:string;
  isLoggedIn:boolean=false;
  constructor(private ruter: Router, public service: MainNavbarService) { }

  LogOut(){
    this.userRole = undefined;
    this.isLoggedIn = false;
    localStorage.setItem('jwt', undefined);
    this.ruter.navigate(['home']); 
  }
  ngOnInit() {
    this.service.newLogin.subscribe(data => {
      if (data) {
        setTimeout(() => {
          this.readLocal();
        }, 100);
        
      }
    })
    this.readLocal();
}

private readLocal(): void 
{

  if(localStorage.getItem('jwt') != "null" && localStorage.getItem('jwt') != "undefined" && localStorage.getItem('jwt') != ""){
    if(localStorage.jwt == undefined)
      return;

    let jwtData = localStorage.jwt.split('.')[1]
    let decodedJwtJsonData = window.atob(jwtData)
    let decodedJwtData = JSON.parse(decodedJwtJsonData)

    if(localStorage.jwt !== undefined){
      this.isLoggedIn = true;
      this.userRole = decodedJwtData.role;
    }
  }
}
}
