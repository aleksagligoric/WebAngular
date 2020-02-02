import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { Observable } from 'rxjs';

@Component({
  selector: 'app-main-navbar',
  templateUrl: './main-navbar.component.html',
  styleUrls: ['./main-navbar.component.css']
})
export class MainNavbarComponent implements OnInit {

  userRole:string;
  isLoggedIn:boolean=false;
  constructor(private ruter: Router) { }

  LogOut(){
    localStorage.setItem('jwt', undefined);
    this.ruter.navigate(['home']); 
  }
  ngOnInit() {
    if(localStorage.getItem('jwt') != "null" && localStorage.getItem('jwt') != "undefined" && localStorage.getItem('jwt') != ""){
            let jwtData = localStorage.jwt.split('.')[1]
            let decodedJwtJsonData = window.atob(jwtData)
            let decodedJwtData = JSON.parse(decodedJwtJsonData)

            if(localStorage.jwt !== undefined){
              this.isLoggedIn = true;
              this.userRole = decodedJwtData.role;
            }
    }
  
  //his.verifikovan = null;
}
}
