import { InvoiceType } from "../enums/invoice-type.enum";
import { Contractor } from "./contractor";

export class Invoice {
  id: number;
  invoiceNumber: string;
  contractor: Contractor;
  date: Date;
  grossAmount: number;
  vatRate: number;
  netAmount: number;
  vatAmount: number;
  type: InvoiceType;
  filePath: string;
}
