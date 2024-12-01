import { Component, OnInit } from '@angular/core';
import { Booking } from '../types/booking';
import { BookingService } from '../booking.service';
import { CommonModule } from '@angular/common';

@Component({
  selector: 'app-mybookings',
  standalone: true,
  imports: [CommonModule],
  templateUrl: './mybookings.component.html',
  styleUrl: './mybookings.component.css'
})
export class MyBookingsComponent implements OnInit {
  bookings: Booking[] = [];  
  isLoading = true;
  errorMessage: string = '';

  constructor(
    private bookingService: BookingService
  ) {}

  ngOnInit(): void {
   
  }

  // cancelBooking(bookingId: number): void {
  //   if (confirm('Are you sure you want to cancel this booking?')) {
  //     this.bookingService.cancelBooking(bookingId).subscribe(
  //       () => {
  //         this.bookings = this.bookings.filter(b => b.id !== bookingId); // Remove cancelled booking from list
  //         alert('Booking cancelled successfully!');
  //       },
  //       (error) => {
  //         alert('Failed to cancel booking.');
  //       }
  //     );
  //   }
  // }
}