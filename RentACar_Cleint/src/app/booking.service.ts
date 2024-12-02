// src/app/services/booking.service.ts
import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { Booking } from './types/booking';

@Injectable({
  providedIn: 'root',
})
export class BookingService {
  private apiUrl = 'https://localhost:7016/api/Order'; 

  constructor(private http: HttpClient) {}

  getBookingsByRenterId(renterId: number): Observable<Booking[]> {
    return this.http.get<Booking[]>(`${this.apiUrl}/Renter/${renterId}`);
  }

  cancelBooking(bookingId: number): Observable<void> {
    return this.http.delete<void>(`${this.apiUrl}/${bookingId}`);
  }
}
