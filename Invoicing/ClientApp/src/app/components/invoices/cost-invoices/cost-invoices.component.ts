import { Component, OnInit } from '@angular/core';
import { InvoicesService} from '../../../services/invoices.service';

@Component({
  selector: 'app-cost-invoices',
  templateUrl: './cost-invoices.component.html',
  styleUrls: ['./cost-invoices.component.css']
})
export class CostInvoicesComponent implements OnInit {

  constructor(private invoicesSevice: InvoicesService) { }

  ngOnInit() {
  }


}
