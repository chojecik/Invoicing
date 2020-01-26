import { Component, OnInit } from '@angular/core';
import { AuthService } from '../../services/auth.service';
import { AccountService } from '../../services/account.service';



@Component({
  selector: 'app-navbar',
  templateUrl: './navbar.component.html',
  styleUrls: ['./navbar.component.css']
})
export class NavbarComponent{
  private firstName: string;
  private email: string;
  private isLoggedIn: boolean;

  constructor(private authService: AuthService, private accountService: AccountService) {
    
  }
}
