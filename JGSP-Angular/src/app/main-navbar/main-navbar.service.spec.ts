import { TestBed } from '@angular/core/testing';

import { MainNavbarService } from './main-navbar.service';

describe('MainNavbarService', () => {
  beforeEach(() => TestBed.configureTestingModule({}));

  it('should be created', () => {
    const service: MainNavbarService = TestBed.get(MainNavbarService);
    expect(service).toBeTruthy();
  });
});
