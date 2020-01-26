import { Injectable } from '@angular/core';
import { LoginInterface} from '../../app/interfaces/login-interface';
import { BehaviorSubject } from 'rxjs';
import { AuthModel } from '../models/auth-model';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { map } from 'rxjs/operators';
import { AppUser } from '../models/app-user';
import { Router } from '@angular/router';
import {StorageService } from '../services/storage.service';

@Injectable({
  providedIn: 'root'
})
export class AuthService {
  url: string = "api/authentication";
  private _authNavStatusSource = new BehaviorSubject<boolean>(false);
  private loggedIn = false;

  authNavStatus$ = this._authNavStatusSource.asObservable();

  constructor(private http: HttpClient, private router: Router, private storageService: StorageService) {
    this.loggedIn = !!localStorage.getItem('auth_token');
    this._authNavStatusSource.next(this.loggedIn);
  }


  login(authModel: AuthModel) {
    let authUrl = this.url + "/login";
    let body = JSON.stringify(authModel);
    const httpOptions = {
      headers: new HttpHeaders({
        'Content-Type': 'application/json'
      })
    };

    return this.http.post<LoginInterface>(authUrl, body, httpOptions)
      .pipe(map((user: AppUser) => {
        // login successful if there's a jwt token in the response
        if (user && user.token) {
          // store user details and jwt token in local storage to keep user logged in between page refreshes
          this.storageService.setItem('auth_token', user.token);
          this.storageService.setItem('email', user.email);
          this.storageService.setItem('firstName', user.firstName);
          this.loggedIn = true;
          this._authNavStatusSource.next(true);
        }
      }));
  }

  logout() {
    this.storageService.removeItem('auth_token');
    this.storageService.removeItem('email');
    this.storageService.removeItem('firstName');
    this.loggedIn = false;
    this._authNavStatusSource.next(false);
    this.router.navigate(['/welcome']);
  }

  isLoggedIn(): boolean {
    return this.loggedIn;
  }

  getEmail(): string {
    return this.storageService.getItem('email');
  }
}


