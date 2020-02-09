import { Component, OnInit } from '@angular/core';
import { FormGroup, FormBuilder, Validators, NgForm } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { InvoicesService } from '../../../services/invoices.service';
import { Invoice } from '../../../models/invoice';
import { DatePipe } from '@angular/common';

@Component({
  selector: 'app-edit-invoice',
  templateUrl: './edit-invoice.component.html',
  styleUrls: ['./edit-invoice.component.css']
})
export class EditInvoiceComponent implements OnInit {
  invoiceForm: FormGroup;
  id: number = null;
  filePath: string;
  vatRates: number[] = [23, 8, 5, 0];
  grossAmountCalculated: number;
  vatAmountCalculated: number;
  vatValue: number;
  netValue: number;
  invoice: Invoice = new Invoice();
  invoiceType: number;

  constructor(
    private formBuilder: FormBuilder,
    private activeRouter: ActivatedRoute,
    private router: Router,
    private invoicesSevice: InvoicesService,
    private datePipe: DatePipe) { }

  ngOnInit() {
    this.getDetail(this.activeRouter.snapshot.params['id']);

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

  getDetail(id) {
    this.invoicesSevice.getInvoice(id)
      .subscribe(data => {
        this.invoiceForm.setValue({
          invoiceNumber: data.invoiceNumber,
          contractor: data.contractor,
          date: this.datePipe.transform(data.date, "yyyy-MM-dd"),
          netAmount: data.netAmount,
          vatRate: data.vatRate,
          grossAmount: data.grossAmount,
          vatAmount: data.vatAmount,
          type: data.type,
          filePath: data.filePath
        });
        this.invoiceType = data.type;
        this.vatValue = data.vatRate;
        this.netValue = data.netAmount;
      });
  }

  updateInvoice(form: NgForm) {

    this.invoicesSevice.updateInvoice(this.id, form)
      .subscribe(res => {
        this.router.navigate(['/']);
      }, (err) => {
        console.log(err);
      });
  }

  netAmountChanged(event) {
    debugger;
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
