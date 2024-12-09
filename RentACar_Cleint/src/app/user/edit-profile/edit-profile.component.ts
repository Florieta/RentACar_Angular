import { Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { ProfileService } from '../profile.service';
import { Router } from '@angular/router';

@Component({
  selector: 'app-edit-profile',
  standalone: true,
  imports: [CommonModule, FormsModule],
  templateUrl: './edit-profile.component.html',
  styleUrls: ['./edit-profile.component.css']
})
export class EditProfileComponent implements OnInit {
  profileType: 'dealer' | 'renter' = 'dealer';
  profile: any = {};
  user: any = null;

  constructor(
    private router: Router,
    private profileService: ProfileService
  ) {}

  ngOnInit() {
    const savedUser = localStorage.getItem('user');
    if (savedUser) {
      const parsedUser = JSON.parse(savedUser);
      this.user = parsedUser?.user;
      if (this.user) {
        if (this.user.dealerId) {
          this.profileType = 'dealer';
          this.loadProfileFromServer(this.user.dealerId, 'dealer');
        } else if (this.user.renterId) {
          this.profileType = 'renter';
          this.loadProfileFromServer(this.user.renterId, 'renter');
        }
      } else {
        console.error('No user data found in localStorage. Redirecting...');
        this.router.navigate(['/my-profile']);
      }
    } else {
      console.error('No saved user in localStorage. Redirecting...');
      this.router.navigate(['/my-profile']);
    }
  }

  loadProfileFromServer(profileId: string, type: 'dealer' | 'renter') {
    if (type === 'dealer') {
      this.profileService.getDealerProfileById(profileId).subscribe(
        (dealerProfile) => {
          console.log('Loaded Dealer Profile:', dealerProfile);
          this.profile = { ...dealerProfile, id: this.user.id, dealerId: this.user.dealerId }; 
          console.log('Loaded Dealer Profile:', dealerProfile);
        },
        (error) => {
          console.error('Error loading dealer profile:', error);
          this.router.navigate(['/my-profile']);
        }
      );
    } else if (type === 'renter') {
      this.profileService.getRenterProfileById(profileId).subscribe(
        (renterProfile) => {
          console.log('Loaded Renter Profile:', renterProfile);
          this.profile = { ...renterProfile, id: this.user.id, renterId: this.user.renterId };
          if (this.profile.expiredDate) {
            this.profile.expiredDate = this.formatDate(this.profile.expiredDate);
          }
        },
        (error) => {
          console.error('Error loading renter profile:', error);
          this.router.navigate(['/my-profile']);
        }
      );
    }
  }

  formatDate(dateString: string): string {
    return dateString.split('T')[0];
  }

  saveChanges() {
    if (this.profileType === 'dealer') {
      this.profileService.updateDealerProfile(this.profile).subscribe(
        (updatedProfile) => {
          updatedProfile = { ...updatedProfile, id: this.user.id, dealerId: this.user.dealerId };
          console.log('Updated Dealer Profile:', updatedProfile);
          this.router.navigate(['/my-profile']);
        },
        (error) => {
          console.error('Error updating dealer profile:', error);
        }
      );
    } else if (this.profileType === 'renter') {
      this.profileService.updateRenterProfile(this.profile).subscribe(
        (updatedProfile) => {
          console.log('Updated Renter Profile:', updatedProfile);
          this.router.navigate(['/my-profile']);
        },
        (error) => {
          console.error('Error updating renter profile:', error);
        }
      );
    }
  }  
}
