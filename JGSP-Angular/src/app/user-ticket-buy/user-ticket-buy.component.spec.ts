import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { UserTicketBuyComponent } from './user-ticket-buy.component';

describe('UserTicketBuyComponent', () => {
  let component: UserTicketBuyComponent;
  let fixture: ComponentFixture<UserTicketBuyComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ UserTicketBuyComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(UserTicketBuyComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
