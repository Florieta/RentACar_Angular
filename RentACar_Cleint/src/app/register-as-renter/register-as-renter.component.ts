import { CommonModule } from '@angular/common';
import { Component } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { RouterLink } from '@angular/router';

@Component({
  selector: 'app-register-as-renter',
  standalone: true,
  imports: [CommonModule, FormsModule, RouterLink],
  templateUrl: './register-as-renter.component.html',
  styleUrl: './register-as-renter.component.css'
})
export class RegisterAsRenterComponent {
  formData = {
    userName: '',
    password: '',
    email: '',
    firstName: '',
    lastName: '',
    phoneNumber: '',
    age: '',
    drivingLicenceNumber: '',
    expiredDate: '',
    address: '',
  };

  onSubmit(form: any): void {
    if (form.valid) {
      console.log('Form Submitted:', this.formData);
      alert('Registration successful!');
    } else {
      alert('Please fill in all required fields correctly.');
    }
  }
}
