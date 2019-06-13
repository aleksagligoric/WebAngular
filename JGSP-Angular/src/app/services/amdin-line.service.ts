import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { Line } from '../models/line';

@Injectable()
export class AmdinLineService {
  base_url = "http://localhost:52295"
  constructor(private http: HttpClient){}

  getAllStation() : Observable<any>{
    return Observable.create((observer) => {
        this.http.get<any>(this.base_url + "/api/Station/GetStations").subscribe(data =>{
            observer.next(data);
            observer.complete();
        })
    });   
  }

  addLine(lajna: Line) : Observable<any>{
    return Observable.create((observer) => {
        this.http.post<any>(this.base_url + "/api/Line/AddLine", lajna).subscribe(data =>{
            observer.next(data);
            observer.complete();
        })
    });
}
}