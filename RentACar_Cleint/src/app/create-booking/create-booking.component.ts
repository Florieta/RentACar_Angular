import { Component, OnInit } from '@angular/core';
import { FormsModule, NgForm } from '@angular/forms';
import { ActivatedRoute, Router, RouterLink } from '@angular/router';
import { CarLocation } from '../types/location';
import { BookingService } from '../booking.service';
import { CommonModule } from '@angular/common';
import { UserService } from '../user/user.service';
import { User } from '../types/user';

@Component({
  selector: 'app-booking',
  standalone: true,
  imports: [CommonModule, FormsModule],
  templateUrl: './create-booking.component.html',
  styleUrls: ['./create-booking.component.css']
})
export class BookingComponent implements OnInit {
  formValues = {
    pickUpDateAndTime: '',
    dropOffDateAndTime: '',
    duration: 1,
    totalAmount: 0,
    paymentType: 'card',
    pickUpLocationId: null,
    dropOffLocationId: null,
    carId: 0,
    dailyRate: 0,
    renterId: 0,
  };
  formErrors = {
    duration: ''
  };
  locations: CarLocation[] = [];
  isLoading = false;
  carId: number | null = null;
  dailyRate: number | null = null;
  renterId: number | null = null;
  user: User | null = null;

  constructor(
    private bookingService: BookingService,
    private userService: UserService,
    private router: Router,
    private route: ActivatedRoute 
  ) {}

  ngOnInit(): void {
    this.userService.userObservable.subscribe((user) => {
      this.user = user;
      if(user){
        this.renterId = user?.user.renterId
      }
    });
    this.route.queryParams.subscribe(params => {
      this.carId = +params['carId']; 
      this.dailyRate = +params['dailyRate']; 

      if (this.carId && this.dailyRate) {
        this.formValues.carId = this.carId;
        this.formValues.dailyRate = this.dailyRate;
      }
    });

    this.bookingService.getLocations().subscribe(
      (data) => {
        this.locations = data;
      },
      (error) => {
        console.error('Failed to load locations');
      }
    );
  }

  updateTotalAmount() {
    const duration = this.formValues?.duration || 0;
    this.formValues.totalAmount = duration * (this.formValues.dailyRate || 0); 
  }

  calculateTotalAmount(): void {
    if (this.formValues.duration && this.formValues.dailyRate) {
      this.formValues.totalAmount = this.formValues.duration * this.formValues.dailyRate;
    }
  }

  submitBooking(form: NgForm): void {
    if (form.invalid) {
      console.error('Form is invalid!');
      return;
    }

    const bookingData = { ...this.formValues, renterId: this.renterId };
    this.isLoading = true;  

    this.bookingService.createBooking(bookingData).subscribe(
      (response) => {
        this.isLoading = false;  
        console.log('Booking successful!', response);
        this.router.navigate(['/my-bookings']);  
      },
      (error) => {
        this.isLoading = false;  
        console.error('Booking failed:', error);
      }
    );
  }
}