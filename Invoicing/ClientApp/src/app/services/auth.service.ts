import { Injectable } from '@angular/core';
import { LoginInterface} from '../../app/interfaces/login-interface';
import { Observable, throwError } from 'rxjs';
import { AuthModel } from '../models/auth-model';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { catchError } from 'rxjs/operators';

@Injectable({
  providedIn: 'root'
})
export class AuthService {
  url: string = "api/authentication";

  constructor(private http: HttpClient) { }


  login(authModel: AuthModel): Observable<LoginInterface> {
    debugger;
    let authUrl = this.url + "/login";
    let body = JSON.stringify(authModel);
    const httpOptions = {
      headers: new HttpHeaders({
        'Content-Type': 'application/json'
      })
    };

    return this.http.post<LoginInterface>(authUrl, body, httpOptions)
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


