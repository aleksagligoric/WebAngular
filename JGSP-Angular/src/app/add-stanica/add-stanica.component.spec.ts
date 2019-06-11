import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { AddStanicaComponent } from './add-stanica.component';

describe('AddStanicaComponent', () => {
  let component: AddStanicaComponent;
  let fixture: ComponentFixture<AddStanicaComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ AddStanicaComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(AddStanicaComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
