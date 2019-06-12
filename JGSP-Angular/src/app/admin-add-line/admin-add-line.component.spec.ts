import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { AdminAddLineComponent } from './admin-add-line.component';

describe('AdminAddLineComponent', () => {
  let component: AdminAddLineComponent;
  let fixture: ComponentFixture<AdminAddLineComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ AdminAddLineComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(AdminAddLineComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
