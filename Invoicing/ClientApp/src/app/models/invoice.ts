import { InvoiceType } from "../enums/invoice-type.enum";

export class Invoice {
  id: number;
  invoiceNumber: string;
  seller: string;
  date: Date;
  grossAmount: number;
  netAmount: number;
  vatAmount: number;
  type: InvoiceType;
  file: File;
}
