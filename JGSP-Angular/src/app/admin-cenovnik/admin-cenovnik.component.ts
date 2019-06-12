import { Component, OnInit } from '@angular/core';
import { UserType } from '../models/userType';
import { TicketType } from '../models/ticketType';
import { RedVoznjeHttpService } from '../services/redvoznje.service';
import { Pricelist } from '../models/pricelist';
import { AdminCenovnikService } from '../services/admin-cenovnik.service';

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

  constructor(private http:  AdminCenovnikService) { }

  ngOnInit() {

  }
  promeniCenu(){
    this.http.postTicket(this.pricelist).subscribe((data) => {
    });


  }

  ispisCena(){
    this.http.getSelectedCena(this.selectedTicketType.Id, this.selectedUserType.Id).subscribe((data)=>{
      this.pricelist.TicketTypeId = this.selectedTicketType.Id;
      this.pricelist.UserTypeId=this.selectedUserType.Id;
      this.pricelist.Cena = data;
      console.log(this.pricelist.Cena);
      err => console.log(err);
    });

  }

}
