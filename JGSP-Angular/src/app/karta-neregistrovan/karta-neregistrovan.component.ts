import { Component, OnInit } from '@angular/core';
import { AuthHttpService } from 'src/app/services/auth.service';
import { NgForm, Validators } from '@angular/forms';
import { FormBuilder } from '@angular/forms';
import { IPayPalConfig, ICreateOrderRequest } from 'ngx-paypal';
// import email from 'node_modules/emailjs/email.js';
// var server = require('././node_modules/emailjs/smtp/client');
// //var email   = require('emailjs/email');

@Component({
  selector: 'app-karta-neregistrovan',
  templateUrl: './karta-neregistrovan.component.html',
  styleUrls: ['./karta-neregistrovan.component.css']
})
export class KartaNeregistrovanComponent implements OnInit {

  constructor(private http: AuthHttpService, private fb: FormBuilder) { }
  tipovi: string[] = ["Dnevna", "Mesecna", "Godisnja", "Vremenska"];
  public payPalConfig?: IPayPalConfig;
  
  tip: string;
  tipPutnika: string;
  cena1: number;
  potvrda : string;
  user: string;
  price: number;

  regGroup = this.fb.group({
  
    mejl :  ['', Validators.required],
    });

    KupiKartuNeregistrovan(){
    
    this.http.GetKupiKartu(this.tip, this.regGroup.get('mejl').value).subscribe((vaziDo)=>
    {
      this.potvrda = vaziDo;
      console.log(vaziDo);
    });

    
    
 
}

onChange(tipKarte) {
  console.log(tipKarte);
  if(tipKarte == 'Dnevna')
    this.initConfig('1.0');
  else if(tipKarte == 'Mesecna')
    this.initConfig('10.0');
  else if(tipKarte == 'Godisnja')
    this.initConfig('100.0');
  else if(tipKarte == 'Vremenska')
    this.initConfig('0.5');
}
  
ngOnInit() {
  this.initConfig('1.00');
  this.tip="Dnevna";
  }

  private initConfig(price: string): void {
    this.payPalConfig = {
    currency: 'EUR',
    clientId: 'AWu6xVoZcMOa8wkaqwzX_zkHtVHV5b08ICA7C2YK2yfmSv0tpAZxi5yn5ynx1Y6k3siAh18CC0JO5V6Y',
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
      this.http.GetKupiKartu(this.tip, this.regGroup.get('mejl').value).subscribe((vaziDo)=>
      {
        this.potvrda = vaziDo;
      });
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
