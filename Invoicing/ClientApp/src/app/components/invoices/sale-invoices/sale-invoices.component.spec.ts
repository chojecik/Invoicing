import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { SaleInvoicesComponent } from './sale-invoices.component';

describe('SaleInvoicesComponent', () => {
  let component: SaleInvoicesComponent;
  let fixture: ComponentFixture<SaleInvoicesComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ SaleInvoicesComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(SaleInvoicesComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
