import { Component, OnInit } from '@angular/core';
import { TimetableType } from '../models/timetableType';
import { Timetable } from '../models/timetable';
import { Line } from '../models/line';
import { DayType } from '../models/dayType';
import { RedVoznjeInfo } from '../models/redVoznjeInfo';
import { RedVoznjeHttpService } from "../services/redvoznje.service";
import { cenovnikInfo } from '../models/cenovnikInfo';
import { UserType } from '../models/userType';
import { TicketType } from '../models/ticketType';
import { Pricelist } from '../models/pricelist';
import { TouchSequence } from 'selenium-webdriver';

@Component({
  selector: 'app-red-voznje',
  templateUrl: './red-voznje.component.html',
  styleUrls: ['./red-voznje.component.css']
})
export class RedVoznjeComponent implements OnInit {

  redVoznjeInfo:RedVoznjeInfo = new RedVoznjeInfo();
  cenovnikInfo:cenovnikInfo=new cenovnikInfo();
  selectedUserType:UserType=new UserType();
  selectedTicketType:TicketType=new UserType();
  selectedTimetableType: TimetableType = new TimetableType();
  selectedDayType: DayType = new DayType();
  selectedLine: Line = new Line();
  filteredLines: Line[] = [];
  timetable: Timetable = new Timetable();
  pricelist:Pricelist=new Pricelist();

  constructor(private http: RedVoznjeHttpService) { }

  ngOnInit() {
    this.http.getAll().subscribe((redVoznjeInfo) => {
      this.redVoznjeInfo = redVoznjeInfo;
      err => console.log(err);
    });

    this.http.getAllCena().subscribe((cenovnikInfo) => {
      this.cenovnikInfo = cenovnikInfo;
      console.log(this.cenovnikInfo);
      err => console.log(err);
    });
  }

  changeselectedLine(){
    this.filteredLines.splice(0);
    this.redVoznjeInfo.Lines.forEach(element => {
      if(element.SerialNumber == this.selectedLine.SerialNumber){
        this.filteredLines.push(element);
      }
    });
  }

  ispisPolaska(){
    this.http.getSelected(this.selectedTimetableType.Id, this.selectedDayType.Id,this.selectedLine.Id).subscribe((data)=>{
      this.timetable.Times = data;
      console.log(this.timetable);
      err => console.log(err);
    });
  }

  kupiKartu(){
    this.http.postTicket(this.pricelist).subscribe((data)=>{
      err => console.log(err);
    });
  }

  ispisCena(){
    this.http.getSelectedCena(this.selectedTicketType.Id, this.selectedUserType.Id).subscribe((data)=>{
      this.pricelist.TicketType = this.selectedTicketType.Id;
      this.pricelist.UserType=this.selectedUserType.Id;
      this.pricelist.Cena = data;
      console.log(this.pricelist.Cena);
      err => console.log(err);
    });
    
  }
}