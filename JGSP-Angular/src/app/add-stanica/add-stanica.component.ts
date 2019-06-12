import { Component, OnInit } from '@angular/core';
import { Validators, FormBuilder } from '@angular/forms';
import { Router } from '@angular/router';
import {Station} from '../models/station'
import { StationServiceService } from '../services/station-service.service';
import { getNumberOfCurrencyDigits } from '@angular/common';

@Component({
  selector: 'app-add-stanica',
  templateUrl: './add-stanica.component.html',
  styleUrls: ['./add-stanica.component.css']
})

export class AddStanicaComponent implements OnInit {
    stanicaForm = this.fb.group({
      Name: [''],
      Address:  [''],
      X:   [''],
      Y:  [''],
  });


  constructor(private http: StationServiceService,private fb: FormBuilder, private router: Router) { }

  ngOnInit() {
  }

 onSubmit(){
    let regModel: Station = this.stanicaForm.value;

    this.http.AddStation(regModel).subscribe(
      (res) => {
        alert('Ok');
      },
      (err) => {
         alert('error');
      })


    this.router.navigate(["/login"])
    //stanicaForm.reset();
  }
}
