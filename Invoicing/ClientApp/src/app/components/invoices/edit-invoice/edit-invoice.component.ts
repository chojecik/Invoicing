import { Component, OnInit } from '@angular/core';
import { FormGroup, FormBuilder, Validators, NgForm } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { InvoicesService } from '../../../services/invoices.service';
import { Invoice } from '../../../models/invoice';
import { DatePipe } from '@angular/common';
import { Contractor } from '../../../models/contractor';
import { ContractorsService } from '../../../services/contractors.service';

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
  grossValueCalculated: number;
  vatAmountCalculated: number;
  vatValue: number;
  netValue: number;
  invoice: Invoice = new Invoice();
  invoiceType: number;
  contractors: Contractor[];

  constructor(
    private formBuilder: FormBuilder,
    private activeRouter: ActivatedRoute,
    private router: Router,
    private invoicesSevice: InvoicesService,
    private datePipe: DatePipe,
    private contractorsService: ContractorsService) { }

  ngOnInit() {
    this.getUserContractors();
    this.getData(this.activeRouter.snapshot.params['id']);
    this.invoiceForm = this.formBuilder.group({
      invoiceNumber: ['', Validators.required],
      contractor: ['', Validators.required],
      dateOfIssue: ['', Validators.required],
      dateOfService: ['', Validators.required],
      netValue: ['', Validators.required],
      vatRate: ['', Validators.required],
      grossValue: [this.grossValueCalculated],
      vatAmount: [this.vatAmountCalculated],
      type: [this.invoiceType],
      filePath: [this.filePath],
      isPaid: []
    });
  }

  getData(id) {
    this.invoicesSevice.getInvoice(id)
      .subscribe(data => {
        this.invoiceForm.setValue({
          invoiceNumber: data.invoiceNumber,
          contractor: data.contractor,
          dateOfIssue: this.datePipe.transform(data.dateOfIssue, "yyyy-MM-dd"),
          dateOfService: this.datePipe.transform(data.dateOfService, "yyyy-MM-dd"),
          type: data.type,
          filePath: data.filePath,
          netValue: data.netValue,
          vatRate: data.vatRate,
          grossValue: data.grossValue,
          vatAmount: data.vatAmount,
          isPaid: data.isPaid
        });
        this.invoiceType = data.type;
        this.vatValue = data.vatRate;
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

  getUserContractors() {
    this.contractorsService.getUserContractors().subscribe(
      res => {
        this.contractors = res;
      },
      err => {
        console.log(err);
      });
  }
}
