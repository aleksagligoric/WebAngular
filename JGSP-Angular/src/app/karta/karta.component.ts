import { Component, OnInit } from '@angular/core';
import { AuthHttpService } from 'src/app/services/auth.service';
import { PaypalComponent} from './../paypal/paypal.component'
import { IPayPalConfig, ICreateOrderRequest } from 'ngx-paypal';
//import { user } from 'src/app/services/auth.service';

@Component({
  selector: 'app-karta',
  templateUrl: './karta.component.html',
  styleUrls: ['./karta.component.css']
})

export class KartaComponent implements OnInit {
  public payPalConfig?: IPayPalConfig;
  
  constructor(private http: AuthHttpService) { }
  tipovi: string[] = ["Dnevna", "Mesecna", "Godisnja", "Vremenska"];
  cenaKarte: number = 15;
  tip: string;
  tipPutnika: string;
  cena1: number;
  vaziDo1 : string;
  user: string;
  mejl : string;
  cena:string;
  ngOnInit() {
    //this.initConfig('1.0');
    this.cena = undefined;
  }

  onChange(tipKarte) {
    
    let jwtData = localStorage.jwt.split('.')[1];
    let decodedJwtJsonData = window.atob(jwtData);
    let decodedJwtData = JSON.parse(decodedJwtJsonData);

    this.http.GetCenaKarte(tipKarte , decodedJwtData.unique_name).subscribe((cena)=>{
      this.cena=cena;
      this.initConfig(this.cena);
    });
    

  }

  KupiKartu(){
    
      this.http.GetKupiKartu(this.tip, "prazno").subscribe((vaziDo)=>
      {
        this.vaziDo1 = vaziDo;
        alert(vaziDo);
        err => console.log(err);
        
      });
  }

  private initConfig(price: string): void {
    console.log("price " + price);
    this.payPalConfig = {
    currency: 'EUR',
    clientId: 'sb',
    createOrderOnClient: (data) => <ICreateOrderRequest>{
      intent: 'CAPTURE',
      purchase_units: [
        {
          amount: {
            currency_code: 'EUR',
            value: price,
            breakdown: {
              item_total: {
                currency_code: 'EUR',
                value: price
              }
            }
          },
          items: [
            {
              name: 'Enterprise Subscription',
              quantity: '1',
              category: 'DIGITAL_GOODS',
              unit_amount: {
                currency_code: 'EUR',
                value: price,
              },
            }
          ]
        }
      ]
    },
    advanced: {
      commit: 'true'
    },
    style: {
      label: 'paypal',
      layout: 'vertical'
    },
    onApprove: (data, actions) => {
      console.log('onApprove - transaction was approved, but not authorized', data, actions);
      actions.order.get().then(details => {
        console.log('onApprove - you can get full order details inside onApprove: ', details);
      });
    },
    onClientAuthorization: (data) => {
      console.log('onClientAuthorization - you should probably inform your server about completed transaction at this point', data);
      //this.showSuccess = true;
    },
    onCancel: (data, actions) => {
      console.log('OnCancel', data, actions);
    },
    onError: err => {
      console.log('OnError', err);
    },
    onClick: (data, actions) => {
      console.log('onClick', data, actions);
    },
  };
  }
}
