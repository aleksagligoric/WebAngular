import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { HttpClient } from '@angular/common/http';

@Injectable({
  providedIn: 'root'
})
export class ControlorCardVerificationService {
  
  base_url = "http://localhost:52295"
  constructor(private http: HttpClient){ }

  getVerification(TicketId: string) : Observable<any>{
    return Observable.create((observer) => {
        this.http.get<any>(this.base_url + "/api/Controlor/GetVerification"+ `/${TicketId}`).subscribe(data =>{
            observer.next(data);
            observer.complete();
        })
    });
}
}
