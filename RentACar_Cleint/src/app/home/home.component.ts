import { Component } from '@angular/core';
import { Router } from '@angular/router';
import { BackgroundImageDirective } from '../directives/background-image.directive';

@Component({
  selector: 'app-home',
  standalone: true,
  imports: [BackgroundImageDirective],
  templateUrl: './home.component.html',
  styleUrl: './home.component.css'
})
export class HomeComponent {
  constructor(private router: Router) {}

  onRegister() {
    this.router.navigate(['/register/renter']);
  }
}
