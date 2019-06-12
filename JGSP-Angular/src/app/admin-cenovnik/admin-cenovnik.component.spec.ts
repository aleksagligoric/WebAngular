import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { AdminCenovnikComponent } from './admin-cenovnik.component';

describe('AdminCenovnikComponent', () => {
  let component: AdminCenovnikComponent;
  let fixture: ComponentFixture<AdminCenovnikComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ AdminCenovnikComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(AdminCenovnikComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
