import { Component, OnInit } from '@angular/core';
import { Pricelist } from '../models/pricelist';
import { AdminCenovnikService } from '../services/admin-cenovnik.service';
import { TicketType } from '../models/ticketType';
import { cenovnikInfo } from '../models/cenovnikInfo';

@Component({
  selector: 'app-user-ticket-buy',
  templateUrl: './user-ticket-buy.component.html',
  styleUrls: ['./user-ticket-buy.component.css']
})
export class UserTicketBuyComponent implements OnInit {

  pricelist:Pricelist=new Pricelist();
  selectedTicketType:TicketType=new TicketType();
  cenovnikInfo:cenovnikInfo=new cenovnikInfo();
  constructor(private http:  AdminCenovnikService) {}

  ngOnInit() {
    this.http.getTicketUserTypes().subscribe((cenovnikInfo) => {
      this.cenovnikInfo = cenovnikInfo;
      console.log(this.cenovnikInfo);
      err => console.log(err);
    });
  }

  KupiKartu(){
    this.http.httpUserPostTicket(this.selectedTicketType.Id).subscribe(data=>{
      if(data){
        alert("Uspesno ste kupili "+this.selectedTicketType.Name+" kartu!");
      }else{
        alert("Profil vam nije odobren, nemate mogucnost kupovine karata!");
      }
    })
  }
}