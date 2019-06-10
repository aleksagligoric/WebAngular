import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { AdminRedVoznjeControlComponent } from './admin-red-voznje-control.component';

describe('AdminRedVoznjeControlComponent', () => {
  let component: AdminRedVoznjeControlComponent;
  let fixture: ComponentFixture<AdminRedVoznjeControlComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ AdminRedVoznjeControlComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(AdminRedVoznjeControlComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
