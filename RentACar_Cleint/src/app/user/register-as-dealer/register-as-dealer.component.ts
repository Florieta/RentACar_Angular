import { CommonModule } from '@angular/common';
import { Component } from '@angular/core';
import { FormsModule, NgForm } from '@angular/forms';
import { Router, RouterLink } from '@angular/router';
import { DealerRegistrationRequest } from '../../types/dealer-registration-request';
import { UserService } from '../user.service';
import { EmailDirective } from '../../directives/email.directive';
import { DOMAINS } from '../../utils/constants';

@Component({
  selector: 'app-register-as-dealer',
  standalone: true,
  imports: [CommonModule, FormsModule, RouterLink, EmailDirective],
  templateUrl: './register-as-dealer.component.html',
  styleUrl: './register-as-dealer.component.css'
})
export class RegisterAsDealerComponent {
  formData: DealerRegistrationRequest = {
    firstName: '',
    lastName: '',
    userName: '',
    phoneNumber: '',
    email: '',
    password: '',
    companyName: '',
    companyNumber: '',
    address: '',
  };
  domains = DOMAINS;

  constructor(private userService: UserService, private router: Router) {}

  onSubmit(form: NgForm): void {
    if (form.valid) {
      this.userService.registerDealer(this.formData).subscribe(
        (response) => {
          console.log('Dealer registration successful', response);
          this.router.navigate(['/login']);
        },
        (error) => {
          console.error('Dealer registration failed', error);
        }
      );
    }
  }
}
