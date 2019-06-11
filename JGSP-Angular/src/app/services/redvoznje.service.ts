import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { Timetable } from '../models/timetable';

@Injectable()
export class RedVoznjeHttpService{
    base_url = "http://localhost:52295"
    constructor(private http: HttpClient){ }

    /*redVoznjeVremena(username: string, password: string) : Observable<any>{
        return Observable.create((observer) => {
            let data = `username=${username}&password=${password}&grant_type=password`;
            let httpOptions={
                headers:{
                    "Content-type": "application/x-www-form-urlencoded"
                }
            }
            this.http.post<any>(this.base_url + "api/RedVoznje",data,httpOptions).subscribe(data => {
                localStorage.jwt = data.access_token;
                observer.next("uspesno");
                localStorage.setItem("loggedUser",username);
                observer.complete();
            },
            err => {
                console.log(err);
                observer.next("neuspesno");
                observer.complete();
            });
        });
     
    }*/

    getAll() : Observable<any>{
        return Observable.create((observer) => {    
            this.http.get<any>(this.base_url + "/api/RedVoznje/RedVoznjiInfo").subscribe(data =>{
                observer.next(data);
                observer.complete();     
            })             
        });
    }

    getAllCena() : Observable<any>{
        return Observable.create((observer) => {    
            this.http.get<any>(this.base_url + "/api/RedVoznje/cenovnikInfo").subscribe(data =>{
                observer.next(data);
                observer.complete();     
            })             
        });
    }

    getSelected(timetableTypeId: number, dayTypeId: number, lineId: number) : Observable<any>{
        return Observable.create((observer) => {    
            this.http.get<any>(this.base_url + "/api/RedVoznje/IspisReda"+ `/${timetableTypeId}` + `/${dayTypeId}`+ `/${lineId}`).subscribe(data =>{
                observer.next(data);
                observer.complete();     
            })             
        });
    }
    PutTime(timetableTypeId: number, dayTypeId: number, lineId: number, times: string) : Observable<any>{
        let data: Timetable = new Timetable();
        let httpOptions = {
            headers: {
                "Content-type": "application/json"
            }
        }
        data.Id = 5;
        data.DayTypeId = dayTypeId;
        data.LineId = lineId;
        data.TimetableTypeId = timetableTypeId;
        data.Times = times;


        return Observable.create((observer) => {    
            this.http.put<any>(this.base_url + "/api/RedVoznje/PromenaVremena", data, httpOptions).subscribe(data =>{
                observer.next(data);
                observer.complete();     
            })             
        });
    }
    getSelectedCena(TicketTypeId: number, UserTypeId: number) : Observable<any>{
        return Observable.create((observer) => {    
            this.http.get<any>(this.base_url + "/api/RedVoznje/IspisCena"+ `/${TicketTypeId}` + `/${UserTypeId}`).subscribe(data =>{
                observer.next(data);
                observer.complete();     
            })             
        });
    
    }

}