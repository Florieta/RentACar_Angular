import { Component, OnInit } from '@angular/core';
import { Booking } from '../types/booking';
import { BookingService } from '../booking.service';
import { CommonModule } from '@angular/common';
import { UserService } from '../user/user.service';

@Component({
  selector: 'app-mybookings',
  standalone: true,
  imports: [CommonModule],
  templateUrl: './mybookings.component.html',
  styleUrls: ['./mybookings.component.css'], 
})
export class MyBookingsComponent implements OnInit {
  bookings: Booking[] = [];
  isLoading = true;
  errorMessage: string = '';
  renterId: number | null = null;

  constructor(
    private bookingService: BookingService,
    private userService: UserService
  ) {}

  ngOnInit(): void {
    this.userService.userObservable.subscribe((user) => {
      if (user) {
        this.renterId = user.user.dealerId || this.getRenterIdFromLocalStorage();
        if (this.renterId) {
          this.loadBookings();
        } else {
          this.errorMessage = 'You must be logged in as a renter to view your bookings.';
          this.isLoading = false;
        }
      } else {
        this.errorMessage = 'You must be logged in as a renter to view your bookings.';
        this.isLoading = false;
      }
    });
  }

  loadBookings(): void {
    if (this.renterId === null) return;

    this.bookingService.getBookingsByRenterId(this.renterId).subscribe({
      next: (data: Booking[]) => {
        this.bookings = data;
        this.isLoading = false;
      },
      error: (err) => {
        this.errorMessage = 'Failed to load bookings. Please try again later.';
        this.isLoading = false;
        console.error('Error loading bookings:', err);
      },
    });
  }

  private getRenterIdFromLocalStorage(): number | null {
    const storedUser = localStorage.getItem('user');
    if (storedUser) {
      const parsedUser = JSON.parse(storedUser);
      return parsedUser.renterId || null;
    }
    return null;
  }

  cancelBooking(id: number): void {
    if (confirm('Are you sure you want to cancel this booking?')) {
      this.bookingService.cancelBooking(id).subscribe({
        next: () => {
          this.loadBookings();
        },
        error: (err) => {
          this.errorMessage = 'Failed to cancel booking. Please try again later.';
          console.error('Error deleting a booking:', err);
        },
      });
    }
  }
}
