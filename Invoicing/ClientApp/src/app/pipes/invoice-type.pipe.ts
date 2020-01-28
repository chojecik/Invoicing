import { Pipe, PipeTransform } from '@angular/core';
import { InvoiceType } from '../enums/invoice-type.enum';

@Pipe({
  name: 'invoiceType'
})
export class InvoiceTypePipe implements PipeTransform {

  transform(value: number, ...args: any[]): string {
    if (value === InvoiceType.Cost) {
      return 'Faktura kosztowa';
    }
    else if (value === InvoiceType.Sale) {
      return 'Faktura sprzedaży';
    }
    else {
      return '';
    }
  }

}
