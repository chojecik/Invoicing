import { Component, OnInit } from '@angular/core';
import { FormGroup, FormBuilder, Validators, NgForm } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { InvoicesService } from '../../../services/invoices.service';

@Component({
  selector: 'app-edit-invoice',
  templateUrl: './edit-invoice.component.html',
  styleUrls: ['./edit-invoice.component.css']
})
export class EditInvoiceComponent implements OnInit {
  invoiceForm: FormGroup;
  id: number = null;

  constructor(
    private formBuilder: FormBuilder,
    private activeAouter: ActivatedRoute,
    private router: Router,
    private invoicesSevice: InvoicesService) { }

  ngOnInit() {
    this.getDetail(this.activeAouter.snapshot.params['id']);

    this.invoiceForm = this.formBuilder.group({
      title: ['', Validators.compose([Validators.required])],
    });
  }

  getDetail(id) {
    this.invoicesSevice.getInvoice(id)
      .subscribe(data => {
        this.id = data.id;
        this.invoiceForm.setValue({
          title: data.invoiceNumber
        });
        console.log(data);
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
}
