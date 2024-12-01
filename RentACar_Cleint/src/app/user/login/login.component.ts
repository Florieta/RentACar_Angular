import { CommonModule } from '@angular/common';
import { Component } from '@angular/core';
import { Router, RouterLink } from '@angular/router';
import { FormsModule, NgForm } from '@angular/forms';
import { UserService } from '../user.service';

@Component({
  selector: 'app-login',
  standalone: true,
  imports: [CommonModule, FormsModule, RouterLink],
  templateUrl: './login.component.html',
  styleUrl: './login.component.css'
})
export class LoginComponent {
  constructor(private userService: UserService, private router: Router) {}

  login(form: NgForm) {
    if (form.invalid) {
      console.error('Invalid Login Form!');
      return;
    }

    const { userName, password } = form.value;

    this.userService.login(userName, password).subscribe({
      next: (response) => {
        console.log(response)
        localStorage.setItem('token', response.token);
    if (response) {
      localStorage.setItem('user', JSON.stringify(response));
    }

        console.log('Login successful!');
        this.router.navigate(['/home']);
      },
      error: (err) => {
        console.error('Login failed:', err);
      },
    });
  }
}
