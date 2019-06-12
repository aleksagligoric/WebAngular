import { TestBed } from '@angular/core/testing';

import { AdminCenovnikService } from './admin-cenovnik.service';

describe('AdminCenovnikService', () => {
  beforeEach(() => TestBed.configureTestingModule({}));

  it('should be created', () => {
    const service: AdminCenovnikService = TestBed.get(AdminCenovnikService);
    expect(service).toBeTruthy();
  });
});
