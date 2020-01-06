import { Injectable } from '@angular/core';
import { Observable, throwError  } from 'rxjs';
import { UserRegistrationInterface } from '../interfaces/user-registration-interface';
import { HttpClient, HttpHeaders, HttpRequest } from '@angular/common/http';
import { catchError } from 'rxjs/operators';

@Injectable({
  providedIn: 'root'
})
export class AccountService {

  url: string = "api/account";

  constructor(private http: HttpClient) { }


  register(email: string, password: string, firstName: string, lastName: string): Observable<UserRegistrationInterface> {
    
    let registerUrl = this.url + "/register";
    let body = JSON.stringify(
      {
        email,
        password,
        firstName,
        lastName
      });

    const httpOptions = {
      headers: new HttpHeaders({
        'Content-Type': 'application/json'
      })
    };

    return this.http.post<UserRegistrationInterface>(registerUrl, body, httpOptions)
      .pipe(
        catchError(this.handleError));
  }

  handleError(error) {
    let errorMessage = '';
    if (error.error instanceof ErrorEvent) {
      // Get client-side error
      errorMessage = error.error.message;
    } else {
      // Get server-side error
      errorMessage = `Error Code: ${error.status}\nMessage: ${error.message}`;
    }
    console.log(errorMessage);
    return throwError(errorMessage);
  }
}
