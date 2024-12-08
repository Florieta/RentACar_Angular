import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { tap } from 'rxjs/operators';
import { User } from '../types/user';
import { Dealer } from '../types/dealer';
import { Renter } from '../types/renter';

@Injectable({
  providedIn: 'root',
})
export class ProfileService {
  private readonly TOKEN_KEY = 'token';
  private apiUrl = 'https://localhost:7016/api';

  constructor(private http: HttpClient) { }

  getDealerProfileById(userId: string) {
    const token = localStorage.getItem(this.TOKEN_KEY);
    return this.http.get<Dealer>(`${this.apiUrl}/Dealer/${userId}`, {
      headers: { Authorization: `Bearer ${token}` },
    }).pipe(
      tap(
        (dealerProfile) => {
          console.log('Fetched Dealer Profile:', dealerProfile);
        },
        (error) => {
          console.error('Error fetching dealer profile:', error);
        }
      )
    );
  }

  getRenterProfileById(userId: string): Observable<any> {
    const token = localStorage.getItem(this.TOKEN_KEY);
    return this.http.get<any>(`${this.apiUrl}/Renter/${userId}`, {
      headers: { Authorization: `Bearer ${token}` },
    });
  }

  updateDealerProfile(profile: Dealer): Observable<Dealer> {
    const token = localStorage.getItem(this.TOKEN_KEY);
    return this.http.put<Dealer>(`${this.apiUrl}/Authentication/user/dealer/${profile.id}`, profile, {
      headers: {
        Authorization: `Bearer ${token}`,
        'Content-Type': 'application/json'
      }
    }).pipe(
      tap(updatedProfile => {
        console.log('Updated Dealer Profile:', updatedProfile);
      }),
    );
  }

  updateRenterProfile(profile: Renter): Observable<Renter> {
    const token = localStorage.getItem(this.TOKEN_KEY);
    return this.http.put<Renter>(`${this.apiUrl}/Authentication/user/renter/${profile.id}`, profile, {
      headers: {
        Authorization: `Bearer ${token}`,
        'Content-Type': 'application/json'
      }
    }).pipe(
      tap(updatedProfile => {
        console.log('Updated Renter Profile:', updatedProfile);
      }),
    );
  }
}
