import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { RegUser, RegUserImg, Test, LinijaZaHub, User } from 'src/app/osoba';
import { Stanica } from 'src/app/osoba';
import { RedVoznje } from 'src/app/osoba';
import { CenovnikBindingModel } from 'src/app/osoba';
import { Observable } from 'rxjs/internal/Observable';


@Injectable()
export class AuthHttpService{
    base_url = "http://localhost:52295"
  constructor(private http: HttpClient){ }
  
  private httpOptions = {
    headers: new HttpHeaders({
      'Content-Type':  'application/json'
    })
  };

  user: string
    logIn(user: User, callback: any){
        let isDone: boolean = false;
        let data = `username=${user.username}&password=${user.password}&grant_type=password`;
        let httpOptions = {
            headers: {
                "Content-type": "application/x-www-form-urlencoded"
            }
        }

        this.http.post<any>(this.base_url + "/oauth/token", data, httpOptions).subscribe(data => {
            localStorage.jwt = data.access_token;
            let jwtData = localStorage.jwt.split('.')[1]
            let decodedJwtJsonData = window.atob(jwtData)
            let decodedJwtData = JSON.parse(decodedJwtJsonData)

  
            let role = decodedJwtData.role
            this.user = decodedJwtData.unique_name;
            callback(true);
        },
        err=>{
            console.log(err);
            console.log(err.error.error_description);
            callback(false, err.status, err.error.error_description);
        });
        
        // console.log("jwt: " + localStorage.jwt);
        // if(localStorage.jwt == undefined || localStorage.jwt == "undefined"){
        //     isDone = false;
        // }
        // else{
        //     isDone = true;
        // }

      //  return isDone;
        
    }

    reg(data: RegUser) : Observable<any>{
        return this.http.post<any>(this.base_url + "/api/Account/Register", data);
    }

    regImg(data: any, username: string) : Observable<any>{    
        return this.http.post<any>(this.base_url + "/api/Slikas/UploadImage/" + username, data);
    }
    GetMejlovi() : Observable<any>{
        return this.http.get<any>(this.base_url + "/api/Values/GetZahtevi");
    }
    Odobri(mejl :string) : Observable<any>{

        var requestBody= {}
        requestBody['mail']=mejl;

        return this.http.post<any>(this.base_url + "/api/Values/Odobri/", requestBody, this.httpOptions);
    }
    DodajRedVoznje1(red : RedVoznje) : Observable<any>{
        return this.http.post<any>(this.base_url + "/api/Redovi/dodajRed" , red);
    }
    DodajStanicu(stanica : Stanica) : Observable<any>{
        return this.http.post<any>(this.base_url + "/api/Stanicas" , stanica);
    }
    DodajLiniju(broj : string) : Observable<any>{
        return this.http.get<any>(this.base_url + "/api/Linijas/GetLinijaDodaj/"  + broj);
    }
      obrisiCenovnik(id: number) : Observable<any>{
        return this.http.delete<any>(this.base_url + "/api/Cenovniks/" + id);
    }
    obrisiRedVoznje(id: number) : Observable<any>{
        return this.http.delete<any>(this.base_url + "/api/RedVoznjes/" + id);
    }
    DeleteLinija(id: number) : Observable<any>{
        return this.http.delete<any>(this.base_url + "/api/Linijas/" + id);
    }
    DeleteStanica(ime: string) : Observable<any>{
        return this.http.get<any>(this.base_url + "/api/Stanicas/IzbrisiStanicu/" + ime +"/a"+ "/a");
    }
    
    Promeni(data: RegUser) : Observable<any>{
        return this.http.post<any>(this.base_url + "/api/Kartas/PromeniProfil", data);
    }
    DodajCenovnik(cenovnik: CenovnikBindingModel) : Observable<any>{
        return this.http.post<any>(this.base_url + "/PromeniCenovnik",  cenovnik);
    }
    GetPolasci(id: number, dan : string) : Observable<any> {
        return this.http.get<any>(this.base_url + "/api/Linijas/GetLinija/" + id +"/" + dan);
    }

    GetLinije() : Observable<any> {
        return this.http.get<any>(this.base_url + "/api/Linijas/");
    }
    GetStanice() : Observable<any> {
        return this.http.get<any>(this.base_url + "/api/Stanicas/GetStanicee");
    }
    GetSpoji(linija:string, stanica: string) : Observable<any> {
        return this.http.get<any>(this.base_url + "/api/Stanicas/Spoji/" + linija + "/" + stanica );
    }
    GetKorisnika() : Observable<any> {
        return this.http.get<any>(this.base_url + "/api/Kartas/DobaviUsera");
    }
    //samo da se iscita json na serveru i popuni baza
    ParsiranjeJson(id: number, dan : string) : Observable<any> {
        return this.http.get<any>(this.base_url + "/api/Linijas/GetLinija/" + id + "/" + dan + "/" + "str");
    }

    GetCenaKarte(tip: string, mail: string): Observable<any>{

        var requestBody = new Test();
        requestBody.tipKarte = tip;
        requestBody.mejl = mail;

        return this.http.post<any>(this.base_url + "/api/Kartas/GetKarta/", requestBody, this.httpOptions);
    }

    GetCenaKarte2(tip: string, mail: string): Observable<any>{

        var requestBody = new Test();
        requestBody.tipKarte = tip;
        requestBody.mejl = mail;

        return this.http.post<any>(this.base_url + "/api/Kartas/GetKarta2/", requestBody, this.httpOptions);
    }

    GetPromenaCene(tip: string, tipPutnika: string, cena : number): Observable<any>{
        return this.http.get<any>(this.base_url + "/api/Kartas/GetKartaPromenaCene/" + tip + "/" + tipPutnika + "/" + cena);
    }

    
    GetKupiKartu(tipKarte: string, mejl: string): Observable<any>
    {
        var requestBody = new Test();
        requestBody.tipKarte = tipKarte;
        requestBody.mejl = mejl;

        return this.http.post<any>(this.base_url + "/api/Kartas/GetKartaKupi2/", requestBody, this.httpOptions);
    }
    GetKupiKartuNeregistrovan(tipKarte: string, mejl :string): Observable<any>{
       
        return this.http.get<any>(this.base_url + "/api/Kartas/GetKartaKupi2/" + tipKarte + "/"  + mejl);
    }
    GetStanicaCord(idStanice: string): Observable<any>{
        return this.http.get<any>(this.base_url + "/api/Stanicas/GetStanica/" + idStanice);
    }
    GetProveriKartu(idKorisnika: string): Observable<any>{
       
        return this.http.get<any>(this.base_url + "/api/Kartas/GetProveri/" + idKorisnika );
    }
    GetSlika(idKorisnika: string): Observable<any>{
        return this.http.get<any>(this.base_url + "/api/Slikas/GetSlika/" + idKorisnika);
    }
    Verifikovan(): Observable<any>{
        return this.http.get<any>(this.base_url + "/api/Values/Verifikovan");
    }
    StanicaZaHub(lin: LinijaZaHub): Observable<any>{
        let httpOptions = {
            headers:{
                "Content-type":"application/json"
            }
        }
        return this.http.post<any>(this.base_url + "/api/Lokacija/StaniceZaHub", lin, httpOptions);
    }

}