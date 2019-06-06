import { TestBed } from '@angular/core/testing';

import { RedvoznjeService } from './redvoznje.service';

describe('RedvoznjeService', () => {
  beforeEach(() => TestBed.configureTestingModule({}));

  it('should be created', () => {
    const service: RedvoznjeService = TestBed.get(RedvoznjeService);
    expect(service).toBeTruthy();
  });
});
