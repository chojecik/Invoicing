import { Component, OnInit } from '@angular/core';
import { InvoicesService} from '../../../services/invoices.service';
import { Invoice } from '../../../models/invoice';
import { InvoiceType } from '../../../enums/invoice-type.enum';
import { flash } from 'ng-animate';
import { trigger, transition, useAnimation } from '@angular/animations';

@Component({
  selector: 'app-cost-invoices',
  templateUrl: './cost-invoices.component.html',
  styleUrls: ['./cost-invoices.component.css'],
  animations: [
    trigger('flash', [transition('* => *', useAnimation(flash))])
  ]
})
export class CostInvoicesComponent implements OnInit {
  invoices: Invoice[] = [];
  tempInfo: string = "Ładowanie...";

  constructor(private invoicesSevice: InvoicesService) { }

  ngOnInit() {
    this.invoicesSevice.getInvoices(InvoiceType.Cost)
      .subscribe(res => {
        if (res.length === 0) {
          this.tempInfo = "Brak danych";
        }
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
