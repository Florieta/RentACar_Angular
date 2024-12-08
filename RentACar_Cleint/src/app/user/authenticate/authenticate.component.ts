import { Component, OnInit } from '@angular/core';
import { LoaderComponent } from '../../shared/loader/loader.component';
import { UserService } from '../user.service';
import { Router } from '@angular/router';

@Component({
  selector: 'app-authenticate',
  standalone: true,
  imports: [LoaderComponent],
  templateUrl: './authenticate.component.html',
  styleUrl: './authenticate.component.css'
})
export class AuthenticateComponent implements OnInit {
  isAuthenticating = false;

  constructor(private userService: UserService, private router: Router) {}

  ngOnInit(): void {
    this.checkAuthentication();
  }

  private checkAuthentication(): void {
    this.isAuthenticating = true; 
    
    this.userService.loadUserFromLocalStorage();

    this.userService.user$.subscribe({
      next: () => {
        this.isAuthenticating = false; 
      },
      error: () => {
        console.error('Error during authentication.');
        this.isAuthenticating = false;
      },
    });
  }
}