import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable, of } from 'rxjs';
import { Contractor } from '../models/contractor';
import { catchError } from 'rxjs/operators';

@Injectable({
  providedIn: 'root'
})
export class ContractorsService {
  apiUrl: string = "api/contractors";
  constructor(private http: HttpClient) { }

  getUserContractors(): Observable<Contractor[]> {
    return this.http.get<Contractor[]>(this.apiUrl + "/user-contractors")
      .pipe(
        catchError(this.handleError('getUserContractors', [])));
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
