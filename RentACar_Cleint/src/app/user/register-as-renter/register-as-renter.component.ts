import { CommonModule } from '@angular/common';
import { Component } from '@angular/core';
import { FormsModule, NgForm } from '@angular/forms';
import { Router, RouterLink } from '@angular/router';
import { UserService } from '../user.service';
import { RenterRegistrationRequest } from '../../types/renter-registration-request';

@Component({
  selector: 'app-register-as-renter',
  standalone: true,
  imports: [CommonModule, FormsModule, RouterLink],
  templateUrl: './register-as-renter.component.html',
  styleUrl: './register-as-renter.component.css'
})
export class RegisterAsRenterComponent {
  formData: RenterRegistrationRequest = {
    userName: '',
    password: '',
    email: '',
    firstName: '',
    lastName: '',
    phoneNumber: '',
    age: 0,
    drivingLicenceNumber: '',
    expiredDate: '',
    address: '',
  };

  constructor(private userService: UserService, private router: Router) {}

  onSubmit(form: NgForm): void {
    if (form.valid) {
      this.userService.registerRenter(this.formData).subscribe(
        (response) => {
          console.log('Renter registration successful:', response);
          this.router.navigate(['/login']);
        },
        (error) => {
          console.error('Renter registration failed:', error);
          alert('Renter registration failed. Please try again.');
        }
      );
    } else {
      alert('Please fill in all required fields correctly.');
    }
  }
}
