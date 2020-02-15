import { Component, OnInit } from '@angular/core';
import { FormGroup, FormBuilder, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { InvoicesService } from '../../../services/invoices.service';
import { Invoice } from '../../../models/invoice';
import { InvoiceType } from '../../../enums/invoice-type.enum';
import { Contractor } from '../../../models/contractor';
import {ContractorsService } from '../../../services/contractors.service';

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
  grossValueCalculated: number;
  vatAmountCalculated: number;
  vatValue: number;
  netValue: number;
  contractors: Contractor[];
  isContractorFormVisible: boolean = false;
  title: string;

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
      grossValue: [this.grossValueCalculated],
      vatAmount: [this.vatAmountCalculated],
      type: [this.invoiceType],
      filePath: [this.filePath],
      isPaid: [false]
    });
  }

  addInvoice(form) {
    this.invoiceForm.patchValue({ filePath: this.filePath });
    this.invoiceForm.patchValue({ grossValue: this.grossValueCalculated });
    this.invoiceForm.patchValue({ vatAmount: this.vatAmountCalculated });
    this.invoiceForm.patchValue({ vatRate: this.vatValue });
    this.invoiceForm.patchValue({ filePath: this.filePath });
   
      this.invoiceService.addInvoice(this.invoiceForm.value)
        .subscribe(
          res => {
            if (InvoiceType.Cost) {
              this.router.navigate(['/cost-invoices']);
            }
            else if (InvoiceType.Sale) {
              this.router.navigate(['/sale-invoices']);
            }
          },
          err => {
            console.log(err);
          });
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

  showContractorForm(event) {
    this.isContractorFormVisible = true;
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
    }
  }

  
}
