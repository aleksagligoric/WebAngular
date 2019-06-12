import { Component, OnInit } from '@angular/core';
import { AmdinLineService } from '../services/amdin-line.service';

@Component({
  selector: 'app-admin-add-line',
  templateUrl: './admin-add-line.component.html',
  styleUrls: ['./admin-add-line.component.css']
})
export class AdminAddLineComponent implements OnInit {

  AllStation:string;

  constructor(private http: AmdinLineService) { }

  ngOnInit() {

    this.http.getAllStation().subscribe((data) => {
      this.AllStation = data;
      err => console.log(err);
    });
  }

}
