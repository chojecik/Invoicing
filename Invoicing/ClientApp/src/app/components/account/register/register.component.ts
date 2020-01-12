import { Component, OnInit } from '@angular/core';
import { AppUser } from '../../../models/app-user';
import { AccountService } from "../../../services/account.service";
import { Router } from '@angular/router';


@Component({
  selector: 'app-register',
  templateUrl: './register.component.html',
  styleUrls: ['./register.component.css']
})
export class RegisterComponent implements OnInit {
  errors: string | undefined;
  isRequesting: boolean = false;
  submitted: boolean = false;

  constructor(
    private accountService: AccountService,
    private router: Router) { }

  ngOnInit() {
  }

  registerUser({ value, valid }: { value: AppUser, valid: boolean }) {
    this.submitted = true;
    //this.isRequesting = true;
    this.errors = '';

    if (valid) {
      this.accountService.register(   //TODO zmnienić zeby metoda przyjmowala obiekt 
        value.email,
        value.password,
        value.firstName,
        value.lastName)
        .subscribe(
          (result: any) => {
            if (result) {
              this.router.navigate(['/login'], { queryParams: { brandNew: true, email: value.email } });
            }
          },
          errors => this.errors = errors);
    }
  }
}
