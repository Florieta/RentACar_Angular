import { Component } from '@angular/core';
import { Router } from '@angular/router';
import { BackgroundImageDirective } from '../directives/background-image.directive';
import { UserService } from '../user/user.service';

@Component({
  selector: 'app-home',
  standalone: true,
  imports: [BackgroundImageDirective],
  templateUrl: './home.component.html',
  styleUrl: './home.component.css'
})
export class HomeComponent {
  isLogged: boolean = false;

  constructor(private router: Router, private userService: UserService) {
    this.userService.userObservable.subscribe((user) => {
      if (user) {
        this.isLogged = true;
      } else {
        this.isLogged = false;
      }
    });
  }

  onRegister() {
    this.router.navigate(['/register/renter']);
  }
}
