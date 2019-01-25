import { TestBed } from '@angular/core/testing';

import { CuberService } from './cuber.service';

describe('CuberService', () => {
  beforeEach(() => TestBed.configureTestingModule({}));

  it('should be created', () => {
    const service: CuberService = TestBed.get(CuberService);
    expect(service).toBeTruthy();
  });
});
