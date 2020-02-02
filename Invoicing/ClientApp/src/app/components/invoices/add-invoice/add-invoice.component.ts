import { Component, OnInit } from '@angular/core';
import { FormGroup, FormBuilder, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { InvoicesService } from '../../../services/invoices.service';
import { Invoice } from '../../../models/invoice';
import { InvoiceType } from '../../../enums/invoice-type.enum';

@Component({
  selector: 'app-add-invoice',
  templateUrl: './add-invoice.component.html',
  styleUrls: ['./add-invoice.component.css']
})
export class AddInvoiceComponent implements OnInit {
  invoiceForm: FormGroup;
  file: File = null;
  invoice: Invoice = new Invoice();
  invoiceType: number;
  filePath: string;
  vatRates: number[] = [23, 8, 5, 0];
  grossAmountCalculated: number;
  vatAmountCalculated: number;
  vatValue: number;
  netValue: number;

  constructor(private formBuilder: FormBuilder, private router: Router, private invoiceService: InvoicesService) { }

  ngOnInit() {
    this.invoiceType = InvoiceType.Cost;
    this.invoiceForm = this.formBuilder.group({
      invoiceNumber: ['', Validators.required],
      contractor: ['', Validators.required],
      date: ['', Validators.required],
      netAmount: ['', Validators.required],
      vatRate: ['', Validators.required],
      grossAmount: [this.grossAmountCalculated],
      vatAmount: [this.vatAmountCalculated],
      type: [this.invoiceType],
      filePath: [this.filePath]
    });
  }

  addInvoice(form) {
    debugger;
    this.invoiceForm.patchValue({ filePath: this.filePath });
    this.invoiceForm.patchValue({ grossAmount: this.grossAmountCalculated });
    this.invoiceForm.patchValue({ vatAmount: this.vatAmountCalculated });
    this.invoiceForm.patchValue({ vatRate: this.vatValue });
    this.invoiceForm.patchValue({ filePath: this.filePath });
    this.invoiceService.addInvoice(this.invoiceForm.value)
      .subscribe(
        res => {
          this.router.navigate(['/cost-invoices']);
        },
        err => {
          console.log(err);
      });
  }

  uploadFinished = (event) => {
    this.filePath = event;
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
    this.grossAmountCalculated = this.netValue + this.vatAmountCalculated;
  }
}
