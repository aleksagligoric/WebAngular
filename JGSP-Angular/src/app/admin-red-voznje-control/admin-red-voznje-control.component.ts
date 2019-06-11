import { Component, OnInit } from '@angular/core';
import { RedVoznjeHttpService } from '../services/redvoznje.service';
import { RedVoznjeInfo } from '../models/redVoznjeInfo';
import { TimetableType } from '../models/timetableType';
import { DayType } from '../models/dayType';
import { Line } from '../models/line';
import { Timetable } from '../models/timetable';

@Component({
  selector: 'app-admin-red-voznje-control',
  templateUrl: './admin-red-voznje-control.component.html',
  styleUrls: ['./admin-red-voznje-control.component.css']
})
export class AdminRedVoznjeControlComponent implements OnInit {
  
  redVoznjeInfo:RedVoznjeInfo = new RedVoznjeInfo();
  selectedTimetableType: TimetableType = new TimetableType();
  selectedDayType: DayType = new DayType();
  selectedLine: Line = new Line();
  timetable: Timetable = new Timetable();
  filteredLines: Line[] = [];
  times:  string;
  constructor(private http: RedVoznjeHttpService) { }

  ngOnInit() {
    this.http.getAll().subscribe((redVoznjeInfo) => {
      this.redVoznjeInfo = redVoznjeInfo;
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
  ChangeTime(){
    this.http.PutTime(this.selectedTimetableType.Id, this.selectedDayType.Id,this.selectedLine.Id, this.times).subscribe((data)=>{
      this.timetable.Times = data;
      console.log(this.timetable);
      err => console.log(err);
    });
  }

}
