import { TestBed } from '@angular/core/testing';
import { CanActivateFn } from '@angular/router';

import { linkGuardGuard } from './link-guard.guard';

describe('linkGuardGuard', () => {
  const executeGuard: CanActivateFn = (...guardParameters) => 
      TestBed.runInInjectionContext(() => linkGuardGuard(...guardParameters));

  beforeEach(() => {
    TestBed.configureTestingModule({});
  });

  it('should be created', () => {
    expect(executeGuard).toBeTruthy();
  });
});
