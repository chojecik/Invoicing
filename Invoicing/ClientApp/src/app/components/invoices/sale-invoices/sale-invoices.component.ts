import { Component, OnInit } from '@angular/core';
import { Invoice } from '../../../models/invoice';
import { InvoicesService } from '../../../services/invoices.service';
import { InvoiceType } from '../../../enums/invoice-type.enum';
import { flash } from 'ng-animate';
import { trigger, transition, useAnimation } from '@angular/animations';

@Component({
  selector: 'app-sale-invoices',
  templateUrl: './sale-invoices.component.html',
  styleUrls: ['./sale-invoices.component.css'],
  animations: [
    trigger('flash', [transition('* => *', useAnimation(flash))])
  ]
})
export class SaleInvoicesComponent implements OnInit {
  invoices: Invoice[] = [];
  tempInfo: string = "Ładowanie...";

  constructor(private invoicesSevice: InvoicesService) { }

  ngOnInit() {
    this.invoicesSevice.getInvoices(InvoiceType.Sale)
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
    if (window.confirm("Czy na pewno chcesz usunąć tą fakturę?")) {
      this.invoicesSevice.deleteInvoice(id)
        .subscribe(
          () => {
            this.invoices.splice(index, 1);
            if (this.invoices.length === 0) {
              this.tempInfo = "Brak danych";
            }
          },
          (err) => {
            console.log(err);
          }
        );
    }
  }

  downloadFile(invoiceId: number) {

  }

}
