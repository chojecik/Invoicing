import { TestBed } from '@angular/core/testing';

import { InvoiceDetailsService } from './invoice-details.service';

describe('InvoiceDetailsService', () => {
  beforeEach(() => TestBed.configureTestingModule({}));

  it('should be created', () => {
    const service: InvoiceDetailsService = TestBed.get(InvoiceDetailsService);
    expect(service).toBeTruthy();
  });
});
