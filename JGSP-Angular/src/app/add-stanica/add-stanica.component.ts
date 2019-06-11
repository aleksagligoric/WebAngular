import { Component, OnInit } from '@angular/core';
import { Validators, FormBuilder } from '@angular/forms';
import { Router } from '@angular/router';
import {Station} from '../models/station'
import { StationServiceService } from '../services/station-service.service';

@Component({
  selector: 'app-add-stanica',
  templateUrl: './add-stanica.component.html',
  styleUrls: ['./add-stanica.component.css']
})

export class AddStanicaComponent implements OnInit {
  stanicaForm = this.fb.group({
    name: ['', Validators.required],
  })


  constructor(private http: StationServiceService,private fb: FormBuilder, private router: Router) { }

  ngOnInit() {  
  }
  
 onSubmit(){
    let regModel: Station = this.stanicaForm.value;
    this.http.AddStation(regModel);
    
    this.router.navigate(["/login"])
    //stanicaForm.reset();
  }
}
