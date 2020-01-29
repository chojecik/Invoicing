import { UserRegistrationInterface } from '../interfaces/user-registration-interface';

export class AppUser implements UserRegistrationInterface{

  constructor(
    public id: number,
    public email: string,
    public password: string,
    public firstName: string,
    public lastName: string,
    public street: string,
    public streetNumber: number,
    public localNumber: string,
    public city: string,
    public zipCode: string,
    public token: string
  ) { };
}
