import { Component, OnInit } from '@angular/core';
import { FormGroup, FormBuilder, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { InvoicesService } from '../../../services/invoices.service';
import { Invoice } from '../../../models/invoice';
import { InvoiceType } from '../../../enums/invoice-type.enum';
import { Contractor } from '../../../models/contractor';
import {ContractorsService } from '../../../services/contractors.service';
import { InvoiceDetails } from '../../../models/invoice-details';

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
  contractors: Contractor[];
  isContractorFormVisible: boolean = false;
  title: string;
  isGenerating: boolean = false;
  isInvoiceDetailsFormVisible: boolean = false;
  details: InvoiceDetails[] = new Array();

  constructor(
    private formBuilder: FormBuilder,
    private router: Router,
    private invoiceService: InvoicesService,
    private contractorsService: ContractorsService) { }

  ngOnInit() {
    this.getUserContractors();
    this.invoiceType = this.getInvoiceTypeFromUrl();
    this.invoiceForm = this.formBuilder.group({
      invoiceNumber: ['', Validators.required],
      contractor: [Contractor, Validators.required],
      dateOfIssue: ['', Validators.required],
      dateOfService: ['', Validators.required],
      netValue: ['', Validators.required],
      vatRate: ['', Validators.required],
      grossAmount: [this.grossAmountCalculated],
      vatAmount: [this.vatAmountCalculated],
      type: [this.invoiceType],
      filePath: [this.filePath],
      isPaid: [false],
      details: [{}]
    });
  }

  addInvoice(form) {
    this.invoiceForm.patchValue({ filePath: this.filePath });
    this.invoiceForm.patchValue({ grossAmount: this.grossAmountCalculated });
    this.invoiceForm.patchValue({ vatAmount: this.vatAmountCalculated });
    this.invoiceForm.patchValue({ vatRate: this.vatValue });
    this.invoiceForm.patchValue({ filePath: this.filePath });
    this.invoiceForm.patchValue({ details: this.details })
    if (this.isGenerating) {
      this.invoiceService.generateInvoice(this.invoiceForm.value)
        .subscribe(
          res => {

          },
          err => {
          });
    }
    else {
      this.invoiceService.addInvoice(this.invoiceForm.value)
        .subscribe(
          res => {
            this.router.navigate(['/cost-invoices']);
          },
          err => {
            console.log(err);
          });
    }
  }

  uploadFinished = (event) => {
    this.filePath = event;
  }

  contractorAddingFinished = (event) => {
    if (event === "close") {
      this.isContractorFormVisible = false;
    }
    else if (this.isNumber(event)) {
      this.isContractorFormVisible = false;
      this.getUserContractors();
    }
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

  getUserContractors() {
    this.contractorsService.getUserContractors().subscribe(
      res => {
        this.contractors = res;
      },
      err => {
        console.log(err);
      });
  }

  showContractorForm(event) {
    this.isContractorFormVisible = true;
  }

  addInvoiceDetail(event) {
    this.isInvoiceDetailsFormVisible = true;
  }

  isNumber(value: string | number): boolean {
  return ((value != null) &&
    (value !== '') &&
    !isNaN(Number(value.toString())));
  }

  getInvoiceTypeFromUrl(): InvoiceType {
    switch (this.router.url) {
      case "/cost-invoices/add":
        this.title = "Dodaj fakturę kosztową";
        return InvoiceType.Cost;
      case "/sale-invoices/add":
        this.title = "Dodaj fakturę sprzedaży";
        return InvoiceType.Sale;
      case "/generate-invoice":
        this.title = "Generuj nową fakturę sprzedaży";
        this.isGenerating = true;
        return InvoiceType.Sale;
    }
  }

  detailAdded = (event: InvoiceDetails) => {
    if (event == null) {
      this.isInvoiceDetailsFormVisible = false;
    }
    else {
      this.details.push(event);
      this.invoice.details = this.details;
      this.isInvoiceDetailsFormVisible = false;
    }
  }
}
