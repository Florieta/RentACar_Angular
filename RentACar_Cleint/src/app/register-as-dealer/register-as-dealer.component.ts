import { CommonModule } from '@angular/common';
import { Component } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { RouterLink } from '@angular/router';

@Component({
  selector: 'app-register-as-dealer',
  standalone: true,
  imports: [CommonModule, FormsModule, RouterLink],
  templateUrl: './register-as-dealer.component.html',
  styleUrl: './register-as-dealer.component.css'
})
export class RegisterAsDealerComponent {
  formData = {
    firstName: '',
    lastName: '',
    userName: '',
    phoneNumber: '',
    email: '',
    password: '',
    companyName: '',
    companyNumber: '',
    address: ''
  };

  onSubmit(form: any): void {
    if (form.valid) {
      console.log('Form Submitted', this.formData);
      alert('Registration successful!');
    } else {
      alert('Please fill in all required fields correctly.');
    }
  }
}
