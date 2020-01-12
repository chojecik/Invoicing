import { LoginInterface} from "../interfaces/login-interface";

export class AuthModel implements LoginInterface {
  constructor(
    public email: string,
    public password: string
  ) { };
}
