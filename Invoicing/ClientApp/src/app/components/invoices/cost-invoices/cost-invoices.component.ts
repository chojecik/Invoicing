import { Component, OnInit } from '@angular/core';
import { InvoicesService} from '../../../services/invoices.service';
import { Invoice } from '../../../models/invoice';
import { InvoiceType } from '../../../enums/invoice-type.enum';

@Component({
  selector: 'app-cost-invoices',
  templateUrl: './cost-invoices.component.html',
  styleUrls: ['./cost-invoices.component.css']
})
export class CostInvoicesComponent implements OnInit {
  invoices: Invoice[] = [];

  constructor(private invoicesSevice: InvoicesService) { }

  ngOnInit() {
    this.invoicesSevice.getInvoices(InvoiceType.Cost)
      .subscribe(res => {
        this.invoices = res;
      }, err => {
        console.log(err);
      });
  }

  deleteInvoice(id, index) {
    this.invoicesSevice.deleteInvoice(id)
      .subscribe(res => {
        this.invoices.splice(index, 1);
      }, (err) => {
        console.log(err);
      }
      );
  }

  downloadFile(invoiceId: number) {

  }

}
