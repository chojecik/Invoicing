import { Component, OnInit, Output } from '@angular/core';
import { FormGroup, FormBuilder, Validators } from '@angular/forms';
import { EventEmitter } from '@angular/core';
import { InvoiceDetailsService } from '../../../services/invoice-details.service';
import { InvoiceDetails } from '../../../models/invoice-details';

@Component({
  selector: 'invoice-details',
  templateUrl: './invoice-details.component.html',
  styleUrls: ['./invoice-details.component.css']
})
export class InvoiceDetailsComponent implements OnInit {
  invoiceDetailsForm: FormGroup;
  vatRates: number[] = [23, 8, 5, 0];
  grossValueCalculated: number;
  vatAmountCalculated: number;
  vatValue: number;
  netValue: number;

  @Output() public onAddingFinished = new EventEmitter<InvoiceDetails>();

  constructor(private formBuilder: FormBuilder, private detailsService: InvoiceDetailsService) { }

  ngOnInit() {
    this.invoiceDetailsForm = this.formBuilder.group({
      productName: ['', Validators.required],
      pkwiu: [''],
      netPrice: ['', Validators.required],
      quantity: ['', Validators.required],
      unit: ['', Validators.required],
      netValue: ['', Validators.required],
      vatRate: ['', Validators.required],
      vatAmount: [this.vatAmountCalculated],
      grossValue: [this.grossValueCalculated]
    })
  }

  addInvoiceDetail() {
    this.invoiceDetailsForm.patchValue({ vatRate: this.vatValue });
    this.invoiceDetailsForm.patchValue({ vatAmount: this.vatAmountCalculated });
    this.invoiceDetailsForm.patchValue({ grossValue: this.grossValueCalculated });


    this.detailsService.addInvoiceDetails(this.invoiceDetailsForm.value).subscribe(
      event => {
        debugger;
        this.onAddingFinished.emit(event);
      },
      err => {
        console.log(err);
        this.onAddingFinished.emit(null);
      });
  }

  closeForm(event) {
    this.onAddingFinished.emit(null);
  }

  netAmountChanged(event) {
    this.netValue = Number(event.target.value);
    this.calculateReadonlyValues();
  }

  vatRateChanged(event) {
    this.vatValue = Number(event.target.value);
    this.calculateReadonlyValues();
  }

  calculateReadonlyValues() {
    this.vatAmountCalculated = Math.round(((this.netValue * this.vatValue / 100) + Number.EPSILON) * 100) / 100
    this.grossValueCalculated = this.netValue + this.vatAmountCalculated;
  }
}
