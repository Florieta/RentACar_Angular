import { Component } from '@angular/core';
import { RouterLink } from '@angular/router';
import { UserService } from '../user/user.service';

@Component({
  selector: 'app-navigation',
  standalone: true,
  imports: [RouterLink],
  templateUrl: './navigation.component.html',
  styleUrl: './navigation.component.css'
})
export class NavigationComponent {
  isLogged: boolean = false;

  constructor(private userService: UserService) {
    this.isLogged = this.userService.isLogged; 
    this.userService.userObservable.subscribe((user) => {
      this.isLogged = !!user;
    });
  }

  logout(): void {
    this.userService.logout().subscribe(() => {
      console.log('Logged out successfully');
    });
  }
}
