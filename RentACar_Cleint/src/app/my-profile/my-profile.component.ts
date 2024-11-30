import { Component } from '@angular/core';
import { Router } from '@angular/router';

@Component({
  selector: 'app-my-profile',
  standalone: true,
  templateUrl: './my-profile.component.html',
  styleUrls: ['./my-profile.component.css'],
})
export class MyProfileComponent {
  user = {
    firstName: 'John',
    lastName: 'Doe',
    email: 'johndoe@example.com',
    userName: 'john_doe123',
    phoneNumber: '+1234567890',
    renterId: true, 
  };

  constructor(private router: Router) {}

  navigateToProfile() {
    if (this.user.renterId) {
      this.router.navigate(['/profile-renter']);
    } else {
      this.router.navigate(['/profile-dealer']);
    }
  }
}
