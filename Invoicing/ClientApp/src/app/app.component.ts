import { Component } from '@angular/core';
import { AuthService} from '../app/services/auth.service';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html'
})
export class AppComponent {
  title = 'app';

  constructor(private authService: AuthService) {}
}
