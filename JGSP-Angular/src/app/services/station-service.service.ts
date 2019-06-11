import { Injectable } from '@angular/core';
import { Station } from '../models/station';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';

@Injectable()
export class StationServiceService {
  base_url = "http://localhost:52295"


  constructor(private http: HttpClient) { }

  AddStation(data: Station): Observable<Response> {        
    return this.http.post<any>(this.base_url + "/api/Station/AddStation",data );
  }
}
