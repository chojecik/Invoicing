import { Component, OnInit } from '@angular/core';
import { FormGroup, FormBuilder, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { InvoicesService } from '../../../services/invoices.service';

@Component({
  selector: 'app-add-invoice',
  templateUrl: './add-invoice.component.html',
  styleUrls: ['./add-invoice.component.css']
})
export class AddInvoiceComponent implements OnInit {
  invoiceForm: FormGroup;

  constructor(private formBuilder: FormBuilder, private router: Router, private invoiceService: InvoicesService) { }

  ngOnInit() {
    this.invoiceForm = this.formBuilder.group({
      title: ['', Validators.compose([Validators.required])],
    });
  }

  addInvoice() {
    const payload = {
      title: this.invoiceForm.controls.title.value,
    };

    this.invoiceService.addInvoice(payload)
      .subscribe(res => {
        let id = res['_id'];
        this.router.navigate(['/']);
      }, (err) => {
        console.log(err);
      });
  }

}
