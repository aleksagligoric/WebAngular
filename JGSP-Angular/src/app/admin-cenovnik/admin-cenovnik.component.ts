import { Component, OnInit } from '@angular/core';
import { UserType } from '../models/userType';
import { TicketType } from '../models/ticketType';
import { RedVoznjeHttpService } from '../services/redvoznje.service';
import { Pricelist } from '../models/pricelist';
import { AdminCenovnikService } from '../services/admin-cenovnik.service';
import { cenovnikInfo } from '../models/cenovnikInfo';

@Component({
  selector: 'app-admin-cenovnik',
  templateUrl: './admin-cenovnik.component.html',
  styleUrls: ['./admin-cenovnik.component.css']
})
export class AdminCenovnikComponent implements OnInit {

  selectedUserType:UserType=new UserType();
  selectedTicketType:TicketType=new UserType();
  pricelist:Pricelist=new Pricelist();
  ticketPrice: String = new String();
  cenovnikInfo:cenovnikInfo=new cenovnikInfo();
  novaCena: number;

  constructor(private http:  AdminCenovnikService) { }

  ngOnInit() {

    this.http.getTicketUserTypes().subscribe((cenovnikInfo) => {
      this.cenovnikInfo = cenovnikInfo;
      console.log(this.cenovnikInfo);
      err => console.log(err);
    });
  }

  promeniCenu(){
    this.pricelist.Price = +this.novaCena;

    this.http.postTicket(this.pricelist).subscribe((data) => {
    });
  }

  ispisCena(){
    this.http.getSelectedCena(this.selectedTicketType.Id, this.selectedUserType.Id).subscribe((data)=>{
      this.pricelist.TicketTypeId = this.selectedTicketType.Id;
      this.pricelist.UserTypeId=this.selectedUserType.Id;
      this.pricelist.Price = data;
      console.log(this.pricelist.Price);
      err => console.log(err);
    });

  }


}
