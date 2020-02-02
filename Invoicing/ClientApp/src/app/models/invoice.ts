import { InvoiceType } from "../enums/invoice-type.enum";

export class Invoice {
  id: number;
  invoiceNumber: string;
  contractor: string;
  date: Date;
  grossAmount: number;
  vatRate: number;
  netAmount: number;
  vatAmount: number;
  type: InvoiceType;
  filePath: string;
}
