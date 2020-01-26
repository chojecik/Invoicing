import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { CostInvoicesComponent } from './cost-invoices.component';

describe('CostInvoicesComponent', () => {
  let component: CostInvoicesComponent;
  let fixture: ComponentFixture<CostInvoicesComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ CostInvoicesComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(CostInvoicesComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
