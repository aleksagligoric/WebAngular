import { TestBed } from '@angular/core/testing';

import { LoginToNavbarService } from './services/login-to-navbar.service';

describe('LoginToNavbarService', () => {
  beforeEach(() => TestBed.configureTestingModule({}));

  it('should be created', () => {
    const service: LoginToNavbarService = TestBed.get(LoginToNavbarService);
    expect(service).toBeTruthy();
  });
});
