import { Injectable } from '@angular/core';
import { BehaviorSubject } from 'rxjs';
import { EventEmitter } from 'protractor';

@Injectable({
  providedIn: 'root'
})
export class MainNavbarService {

  public newLogin: BehaviorSubject<boolean> = new BehaviorSubject(false);

  constructor() { }
}
