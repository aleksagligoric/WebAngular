import { TestBed } from '@angular/core/testing';

import { ControlorCardVerificationService } from './controlor-card-verification.service';

describe('ControlorCardVerificationService', () => {
  beforeEach(() => TestBed.configureTestingModule({}));

  it('should be created', () => {
    const service: ControlorCardVerificationService = TestBed.get(ControlorCardVerificationService);
    expect(service).toBeTruthy();
  });
});
