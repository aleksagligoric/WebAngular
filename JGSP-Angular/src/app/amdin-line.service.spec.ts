import { TestBed } from '@angular/core/testing';

import { AmdinLineService } from './amdin-line.service';

describe('AmdinLineService', () => {
  beforeEach(() => TestBed.configureTestingModule({}));

  it('should be created', () => {
    const service: AmdinLineService = TestBed.get(AmdinLineService);
    expect(service).toBeTruthy();
  });
});
