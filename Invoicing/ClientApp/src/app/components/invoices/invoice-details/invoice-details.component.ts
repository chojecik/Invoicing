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
  grossValueCalculated: number = 0;
  vatAmountCalculated: number = 0;
  netValueCalculated: number = 0;
  vatValue: number = 0;
  netValue: number = 0;
  netPrice: number = 0;
  quantity: number = 0;
  unit: string = "szt.";

  @Output() public onAddingFinished = new EventEmitter<InvoiceDetails>();

  constructor(private formBuilder: FormBuilder, private detailsService: InvoiceDetailsService) { }

  ngOnInit() {
    this.invoiceDetailsForm = this.formBuilder.group({
      productName: ['', Validators.required],
      pkwiu: [''],
      netPrice: ['', Validators.required],
      quantity: ['', Validators.required],
      unit: [this.unit],
      netValue: [this.netValueCalculated],
      vatRate: ['', Validators.required],
      vatAmount: [this.vatAmountCalculated],
      grossValue: [this.grossValueCalculated]
    })
  }

  addInvoiceDetail() {
    this.invoiceDetailsForm.patchValue({ vatRate: this.vatValue });
    this.invoiceDetailsForm.patchValue({ vatAmount: this.vatAmountCalculated });
    this.invoiceDetailsForm.patchValue({ grossValue: this.grossValueCalculated });
    this.invoiceDetailsForm.patchValue({ netValue: this.netValueCalculated });


    this.detailsService.addInvoiceDetails(this.invoiceDetailsForm.value).subscribe(
      event => {
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

  vatRateChanged(event) {
    this.vatValue = Number(event.target.value);
    this.calculateReadonlyValues();
  }

  calculateReadonlyValues() {
    this.netValueCalculated = this.netPrice * this.quantity;
    this.vatAmountCalculated = Math.round(((this.netValueCalculated * this.vatValue / 100) + Number.EPSILON) * 100) / 100
    this.grossValueCalculated = this.netValueCalculated + this.vatAmountCalculated;
  }

  netPriceChanged(event) {
    this.netPrice = event.target.valueAsNumber;
    this.calculateReadonlyValues();
  }

  quantityChanged(event) {
    this.quantity = event.target.valueAsNumber;
    this.calculateReadonlyValues();
  }
}
