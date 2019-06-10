import { Injectable } from '@angular/core';
import {HttpClient,HttpHeaders,HttpResponse} from '@angular/common/http'
import { RegUser } from 'src/app/osoba';
import { Observable } from 'rxjs/internal/Observable';
import {ImageSnippet} from 'src/app/registracija/registracija.component'
import { fromEventPattern } from 'rxjs';



@Injectable()
export class AuthHttpService{
    base_url = "http://localhost:52295"

    constructor(private http: HttpClient){

    }

    logIn(username: string, password: string){

        let data = `username=${username}&password=${password}&grant_type=password`;
        let httpOptions = {
            headers: {
                "Content-type": "application/x-www-form-urlencoded"
            }
        }

        this.http.post<any>(this.base_url + "/oauth/token", data, httpOptions).subscribe(data => {
            localStorage.jwt = data.access_token;
        });
    }

    reg(data: RegUser): Observable<Response> {
     
        const formData = new FormData();

        formData.append('image', Image);
        return this.http.post<any>(this.base_url + "/api/Account/Register",formData,data );
    }
}