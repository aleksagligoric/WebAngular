import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { ControlorCardVerificationComponent } from './controlor-card-verification.component';

describe('ControlorCardVerificationComponent', () => {
  let component: ControlorCardVerificationComponent;
  let fixture: ComponentFixture<ControlorCardVerificationComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ ControlorCardVerificationComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(ControlorCardVerificationComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
