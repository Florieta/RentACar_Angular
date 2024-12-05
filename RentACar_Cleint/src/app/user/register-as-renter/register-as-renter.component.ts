import { CommonModule } from '@angular/common';
import { Component } from '@angular/core';
import { FormsModule, NgForm } from '@angular/forms';
import { Router, RouterLink } from '@angular/router';
import { UserService } from '../user.service';
import { RenterRegistrationRequest } from '../../types/renter-registration-request';
import { DOMAINS } from '../../constants';
import { EmailDirective } from '../../directives/email.directive';

@Component({
  selector: 'app-register-as-renter',
  standalone: true,
  imports: [CommonModule, FormsModule, RouterLink, EmailDirective],
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
  domains = DOMAINS;

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
        }
      );
    }
  }
}
