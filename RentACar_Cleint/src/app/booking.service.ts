import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { Booking } from './types/booking';
import {CarLocation } from './types/location';

@Injectable({
  providedIn: 'root',
})
export class BookingService {
  private apiUrl = 'https://localhost:7016/api'; 

  constructor(private http: HttpClient) {}

  getBookingsByRenterId(renterId: number): Observable<Booking[]> {
    return this.http.get<Booking[]>(`${this.apiUrl}/Order/Renter/${renterId}`);
  }

  cancelBooking(bookingId: number): Observable<void> {
    return this.http.delete<void>(`${this.apiUrl}/Order/${bookingId}`);
  }

  getLocations(): Observable<CarLocation[]> {
    return this.http.get<CarLocation[]>(`${this.apiUrl}/Location`);
  }

  createBooking(bookingData: any): Observable<Booking> {
    return this.http.post<Booking>(`${this.apiUrl}/Order`, bookingData);
  }
}
