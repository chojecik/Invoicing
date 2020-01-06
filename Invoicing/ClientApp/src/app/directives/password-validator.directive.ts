import { Directive, forwardRef } from '@angular/core';
import { NG_VALIDATORS, FormControl } from '@angular/forms';

function validatePasswordFactory() {
  return (c: FormControl) => {
    let PASSWORD_REGEX = /^(?=.*[a-z])(?=.*[A-Z])(?=.*[0-9])(?=.*[!@#\$%\^&\*\_])(?=.{8,})/; //at least 8 characters, one lowercase, one uppercase, one numerical and one special 
    return PASSWORD_REGEX.test(c.value) ? null : {
      validatePassword: {
        valid: false
      }
    };
  };
}

@Directive({
  selector: "[validatePassword][ngModel],[validatePassword][formControl]",
  providers: [
    { provide: NG_VALIDATORS, useExisting: forwardRef(() => PasswordValidatorDirective), multi: true }
  ]
})
export class PasswordValidatorDirective {
  validator: Function;

  constructor() {
    this.validator = validatePasswordFactory();
  }

  validate(formControl: FormControl) {
    return this.validator(formControl);
  }
}
