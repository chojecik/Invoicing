import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { InvoiceDetails } from '../models/invoice-details';
import { Observable, of } from 'rxjs';
import { catchError } from 'rxjs/operators';

@Injectable({
  providedIn: 'root'
})
export class InvoiceDetailsService {
  apiUrl: string = "api/invoiceDetails";

  constructor(private http: HttpClient) { }

  addInvoiceDetails(details: InvoiceDetails): Observable<InvoiceDetails> {
    return this.http.post<InvoiceDetails>(this.apiUrl, details).pipe(catchError(this.handleError<InvoiceDetails>('addInvoiceDetails')));
  }

  private handleError<T>(operation = 'operation', result?: T) {
    return (error: any): Observable<T> => {
      console.error(error); // log to console instead

      return of(result as T);
    };
  }
}
