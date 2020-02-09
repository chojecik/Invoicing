import { Injectable } from '@angular/core';
import { Observable, of } from 'rxjs';
import { Invoice } from '../models/invoice';
import { HttpHeaders, HttpClient } from '@angular/common/http';
import { tap, catchError } from 'rxjs/operators';
import { InvoiceType } from '../enums/invoice-type.enum';
import { StorageService } from './storage.service';

@Injectable({
  providedIn: 'root'
})
export class InvoicesService {
  apiUrl: string = "api/invoices";

  constructor(private http: HttpClient, private storageService: StorageService) {
  }

  getInvoices(type: InvoiceType): Observable<Invoice[]> {
    let url = this.apiUrl + "/invoices/";
    const httpOptions = {
      headers: new HttpHeaders({ 'Content-Type': 'application/json' })
    };

    return this.http.get<Invoice[]>(url + type, httpOptions)
      .pipe(
        catchError(this.handleError('getInvoices', []))
      );
  }

  getInvoice(id: number): Observable<Invoice> {
    const url = `${this.apiUrl}/${id}`;
    return this.http.get<Invoice>(url)
      .pipe(
        catchError(this.handleError<Invoice>(`getInvoice id=${id}`))
    );
  }

  addInvoice(invoice: Invoice): Observable<Invoice> {
    const httpOptions = {
      headers: new HttpHeaders({ 'Content-Type': 'application/json' })
    };

    return this.http.post<Invoice>(`${this.apiUrl}`, invoice, httpOptions).pipe(
      catchError(this.handleError<Invoice>('addInvoice'))
    );
  }

  updateInvoice(id, invoice): Observable<any> {
    const httpOptions = {
      headers: new HttpHeaders({ 'Content-Type': 'application/json' })
    };
    const url = `${this.apiUrl}`;
    return this.http.put(url, invoice, httpOptions).pipe(
      tap(_ => console.log(`updated invoice id=${id}`)),
      catchError(this.handleError<any>('updateInvoice'))
    );
  }

  deleteInvoice(id): Observable<Invoice> {
    const url = `${this.apiUrl}/${id}`;
    const httpOptions = {
      headers: new HttpHeaders({ 'Content-Type': 'application/json' })
    };

    return this.http.delete<Invoice>(url, httpOptions)
      .pipe(
        catchError(this.handleError<Invoice>('deleteInvoice'))
    );
  }
  private handleError<T>(operation = 'operation', result?: T) {
    return (error: any): Observable<T> => {

      // TODO: send the error to remote logging infrastructure
      console.error(error); // log to console instead

      // Let the app keep running by returning an empty result.
      return of(result as T);
    };
  }
}
