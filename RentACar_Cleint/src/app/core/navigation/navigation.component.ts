import { Component, Inject, PLATFORM_ID } from '@angular/core';
import { RouterLink } from '@angular/router';
import { UserService } from '../../user/user.service';
import { isPlatformBrowser } from '@angular/common';

@Component({
  selector: 'app-navigation',
  standalone: true,
  imports: [RouterLink],
  templateUrl: './navigation.component.html',
  styleUrl: './navigation.component.css'
})
export class NavigationComponent {
  isLogged: boolean = false;
  isDealer: boolean = false;
  isRenter: boolean = false;

  constructor(private userService: UserService, @Inject(PLATFORM_ID) private platformId: Object) {

    if (isPlatformBrowser(this.platformId)) {
      const user = localStorage.getItem('user');
      if (user) {
        const parsedUser = JSON.parse(user);
        this.isLogged = true;
        if(this.isDealer = parsedUser?.dealerId){
          this.isDealer = true
        }else{
          this.isRenter = true;
        }
      }
    }

    this.userService.userObservable.subscribe((user) => {
      if (user) {
        this.isLogged = true;
        this.isDealer = user?.user?.dealerId ? true : false; 
      } else {
        this.isLogged = false;
        this.isDealer = false;
      }
    });
  }
  logout(): void {
    this.userService.logout().subscribe(() => {
      console.log('Logged out successfully');
    });
  }
}
