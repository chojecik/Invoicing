import { Component, OnInit } from '@angular/core';
import { InvoiceDetails } from '../../../models/invoice-details';
import { Invoice } from '../../../models/invoice';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { InvoicesService } from '../../../services/invoices.service';
import { ContractorsService } from '../../../services/contractors.service';
import { Contractor } from '../../../models/contractor';
import { InvoiceType } from '../../../enums/invoice-type.enum';

@Component({
  selector: 'app-generate-invoice',
  templateUrl: './generate-invoice.component.html',
  styleUrls: ['./generate-invoice.component.css']
})
export class GenerateInvoiceComponent implements OnInit {
  generateInvoiceForm: FormGroup;
  isInvoiceDetailsFormVisible: boolean = false;
  details: InvoiceDetails[] = new Array();
  title = "Generuj nową fakturę sprzedaży";
  invoice: Invoice = new Invoice();
  contractors: Contractor[];
  grossValueSum: number = 0;
  vatAmountSum: number = 0;
  netValueSum: number = 0;
  invoiceType: InvoiceType;

  constructor(private formBuilder: FormBuilder,
    private router: Router,
    private invoiceService: InvoicesService,
    private contractorsService: ContractorsService) { }

  ngOnInit() {
    this.getUserContractors();
    this.invoiceType = InvoiceType.Sale;
    this.generateInvoiceForm = this.formBuilder.group({
      invoiceNumber: ['', Validators.required],
      contractor: [Contractor, Validators.required],
      dateOfIssue: ['', Validators.required],
      dateOfService: ['', Validators.required],
      netValue: [this.netValueSum],
      grossValue: [this.grossValueSum],
      vatAmount: [this.vatAmountSum],
      type: [this.invoiceType],
      isPaid: [false],
      details: [{}]
    })
  }

  detailAdded = (event: InvoiceDetails) => {
    if (event != null) {
      this.details.push(event);
      this.invoice.details = this.details;
      this.netValueSum += event.netValue;
      this.vatAmountSum += event.vatAmount;
      this.grossValueSum += event.grossValue;
    }
    this.isInvoiceDetailsFormVisible = false;
  }

  generateInvoice(form) {
    this.generateInvoiceForm.patchValue({ netValue: this.netValueSum });
    this.generateInvoiceForm.patchValue({ vatAmount: this.vatAmountSum });
    this.generateInvoiceForm.patchValue({ grossValue: this.grossValueSum });
    this.generateInvoiceForm.patchValue({ grossValue: this.grossValueSum });
    this.generateInvoiceForm.patchValue({ details: this.details });

    this.invoiceService.generateInvoice(this.generateInvoiceForm.value)
      .subscribe(
        res => {
          const data = window.URL.createObjectURL(res);
          var link = document.createElement('a');
          link.href = data;
          link.download = "faktura";
          link.dispatchEvent(new MouseEvent('click', { bubbles: true, cancelable: true, view: window }));
          setTimeout(function () {
            window.URL.revokeObjectURL(data);
            link.remove();
          }, 100);
        },
        err => {
        });
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

  addInvoiceDetail(event) {
    this.isInvoiceDetailsFormVisible = true;
  }
}
