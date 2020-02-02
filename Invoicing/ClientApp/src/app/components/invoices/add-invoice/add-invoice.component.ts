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

  constructor(private formBuilder: FormBuilder, private router: Router, private invoiceService: InvoicesService) { }

  ngOnInit() {
    this.invoiceType = InvoiceType.Cost;
    this.invoiceForm = this.formBuilder.group({
      invoiceNumber: ['', Validators.required],
      contractor: ['', Validators.required],
      date: ['', Validators.required],
      grossAmount: ['', Validators.required],
      netAmount: ['', Validators.required],
      vatAmount: ['', Validators.required],
      type: [this.invoiceType],
      filePath: [this.filePath]
    });
  }

  addInvoice(form) {
    this.invoiceForm.patchValue({ filePath: this.filePath });
    this.invoiceService.addInvoice(this.invoiceForm.value)
      .subscribe(res => {
        let id = res['_id'];
        this.router.navigate(['/']);//TODO navigate to all invoices view on success
      }, (err) => {
        console.log(err);
      });
  }

  uploadFinished = (event) => {
    this.filePath = event;
}
}
