import { Component, OnInit } from '@angular/core';
import { AuthService } from "../../../services/auth.service";
import { Router, ActivatedRoute } from '@angular/router';
import { AuthModel } from '../../../models/auth-model';
import { Subscription } from 'rxjs';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css']
})
export class LoginComponent implements OnInit {
  errors: string;
  email: string;
  private subscription: Subscription = new Subscription();

  constructor(
    private authService: AuthService,
    private router: Router,
    private activatedRoute: ActivatedRoute) { }

  ngOnInit() {
    this.subscription = this.activatedRoute.queryParams.subscribe(
      params => {
        this.email = params["email"];
      })
  }

  login({ value, valid }: { value: AuthModel, valid: boolean }) {
    if (valid) {
      this.authService.login(value)
        .subscribe(
          success => {
            this.router.navigate(['/home']);
          },
          error => {
            this.errors = error;
          })
    }
  }
}
