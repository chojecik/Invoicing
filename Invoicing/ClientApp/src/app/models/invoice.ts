import { InvoiceType } from "../enums/invoice-type.enum";
import { Contractor } from "./contractor";
import { InvoiceDetails } from "./invoice-details";

export class Invoice {
  id: number;
  invoiceNumber: string;
  contractor: Contractor;
  dateOfIssue: Date;
  dateOfService: Date;
  type: InvoiceType;
  isPaid: boolean;
  vatRate: number;
  netValue: number;
  vatAmount: number;
  grossValue: number;
  filePath: string;
  details: InvoiceDetails[];
}
