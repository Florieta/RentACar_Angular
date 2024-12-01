import { Component } from '@angular/core';
import { Router } from '@angular/router';
import { Dealer } from '../../types/dealer';
import { ProfileService } from '../profile.service';
import { Renter } from '../../types/renter';

@Component({
  selector: 'app-my-profile',
  standalone: true,
  templateUrl: './my-profile.component.html',
  styleUrls: ['./my-profile.component.css'],
})
export class MyProfileComponent {
  dealerProfile: Dealer | null = null;  
  renterProfile: Renter | null = null;  
  user: any = null;

  constructor(private router: Router, private profileService: ProfileService) {}

  ngOnInit() {
    const savedUser = localStorage.getItem('user');
    if (savedUser) {
      const parsedUser = JSON.parse(savedUser);
      this.user = parsedUser?.user;

      if (this.user) {
        if (this.user.dealerId) {
          this.profileService.getDealerProfileById(this.user.dealerId).subscribe(
            (dealerData) => {
              this.dealerProfile = dealerData;
            },
            (error) => {
              console.error('Error fetching dealer profile:', error);
            }
          );
        } else if (this.user.renterId) {
          this.profileService.getRenterProfileById(this.user.renterId).subscribe(
            (renterData) => {
              this.renterProfile = renterData;
            },
            (error) => {
              console.error('Error fetching renter profile:', error);
            }
          );
        }
      }
    }
  }

  navigateToProfile() {
    if (this.user?.renterId) {
      this.router.navigate(['/profile-renter']);
    } else {
      this.router.navigate(['/profile-dealer']);
    }
  }
}