import { Component, OnInit } from '@angular/core';
import { AmdinLineService } from '../services/amdin-line.service';
import { Line } from '../models/line';

@Component({
  selector: 'app-admin-add-line',
  templateUrl: './admin-add-line.component.html',
  styleUrls: ['./admin-add-line.component.css']
})
export class AdminAddLineComponent implements OnInit {

  AllStation:string;
  LineStation:string;
  LineName:string;
  lajna: Line;

  constructor(private http: AmdinLineService) { }

  ngOnInit() {

    this.http.getAllStation().subscribe((data) => {
      this.AllStation = data;
      err => console.log(err);
    });
  }


  addLine()
  {
    this.lajna.Stations = this.AllStation;
    this.lajna.SerialNumber = +this.LineName;

    this.http.addLine(this.lajna).subscribe((data) => {

    });

  }
}