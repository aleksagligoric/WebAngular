import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { HttpClient } from '@angular/common/http';
import { Pricelist } from '../models/pricelist';

@Injectable({
  providedIn: 'root'
})
export class AdminCenovnikService {

 
  base_url = "http://localhost:52295"
  constructor(private http: HttpClient){ }


  getSelectedCena(TicketTypeId: number, UserTypeId: number) : Observable<any>{
    return Observable.create((observer) => {
        this.http.get<any>(this.base_url + "/api/Tickets/IspisCena"+ `/${TicketTypeId}` + `/${UserTypeId}`).subscribe(data =>{
            observer.next(data);
            observer.complete();
        })
    });


}


postTicket(postTicket: Pricelist): Observable<any>{
    return Observable.create( (observer) => {
        this.http.post<string>(this.base_url + '/api/Tickets/BuyTicket', postTicket).subscribe(data => {
            observer.next(data);
            observer.complete();
        });
    });
}
}
