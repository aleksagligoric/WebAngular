import { Component, OnInit } from '@angular/core';
import { ControlorCardVerificationService } from '../controlor-card-verification.service';

@Component({
  selector: 'app-controlor-card-verification',
  templateUrl: './controlor-card-verification.component.html',
  styleUrls: ['./controlor-card-verification.component.css']
})
export class ControlorCardVerificationComponent implements OnInit {

  TicketId:string;
  IsValidate:string;

  constructor(private http:ControlorCardVerificationService) { }

  ngOnInit() {
  }

  ispisCena(){
    this.http.getVerification(this.TicketId).subscribe((data)=>{
      this.IsValidate=data;
      err => console.log(err);
    });
  }
}
