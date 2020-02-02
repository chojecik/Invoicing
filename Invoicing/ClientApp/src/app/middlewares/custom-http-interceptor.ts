import { Injectable } from "@angular/core";
import { HttpInterceptor, HttpHeaders } from "@angular/common/http";
import { HttpRequest } from '@angular/common/http';
import { HttpHandler } from '@angular/common/http';
import { HttpEvent } from '@angular/common/http';
import { Observable} from 'rxjs';
import { StorageService } from "../services/storage.service";

@Injectable()
export class CustomHttpInterceptor implements HttpInterceptor{

  constructor(private storageService: StorageService) { }

  intercept(req: HttpRequest<any>, next: HttpHandler): Observable<HttpEvent<any>> {
    const token = this.storageService.getItem("auth_token");
    let changedRequest = req;
    // HttpHeader object immutable - copy values
    const headerSettings: { [name: string]: string | string[]; } = {};

    for (const key of req.headers.keys()) {
      headerSettings[key] = req.headers.getAll(key);
    }
    if (token) {
      headerSettings['Authorization'] = 'Bearer ' + token;
    }
    //headerSettings['Content-Type'] = 'application/json';
    const newHeader = new HttpHeaders(headerSettings);

    changedRequest = req.clone({
      headers: newHeader
    });
    return next.handle(changedRequest);
  }
}
