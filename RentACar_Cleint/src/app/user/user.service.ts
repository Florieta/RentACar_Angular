import { Injectable, OnDestroy, Inject, PLATFORM_ID } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { BehaviorSubject, catchError, Subscription, tap } from 'rxjs';
import { User } from '../types/user';
import { isPlatformBrowser } from '@angular/common';
import { DealerRegistrationRequest } from '../types/dealer-registration-request';
import { RenterRegistrationRequest } from '../types/renter-registration-request';

@Injectable({
  providedIn: 'root',
})
export class UserService implements OnDestroy {
  private user$$ = new BehaviorSubject<User | null>(null);
  private user$ = this.user$$.asObservable();
  private user: User | null = null;
  private userSubscription: Subscription | null = null;
  private readonly TOKEN_KEY = 'token';
  private readonly USER_KEY = 'user';
  private apiUrl = 'https://localhost:7016/api';

  get isLogged(): boolean {
    return !!this.user;
  }

  get userObservable() {
    return this.user$;
  }

  constructor(private http: HttpClient, @Inject(PLATFORM_ID) private platformId: Object) {
    this.userSubscription = this.user$.subscribe((user) => {
      this.user = user;
      if (user) {
        if (isPlatformBrowser(this.platformId)) {
          localStorage.setItem(this.TOKEN_KEY, user.token);
          localStorage.setItem(this.USER_KEY, JSON.stringify(user));
        }
      } else {
        if (isPlatformBrowser(this.platformId)) {
          localStorage.removeItem(this.TOKEN_KEY);
          localStorage.removeItem(this.USER_KEY);
        }
      }
    });

    
    this.loadUserFromLocalStorage();
  }

  private loadUserFromLocalStorage() {
    if (isPlatformBrowser(this.platformId)) {
      const savedUser = localStorage.getItem(this.USER_KEY);
      if (savedUser) {
        this.user$$.next(JSON.parse(savedUser));
      }
    }
  }

  login(userName: string, password: string) {
    return this.http
      .post<User>(`${this.apiUrl}/Authentication/login`, { userName, password })
      .pipe(
        tap((user) => {
          this.user$$.next(user);
          if (isPlatformBrowser(this.platformId)) {
            localStorage.setItem(this.USER_KEY, JSON.stringify(user));
            localStorage.setItem(this.TOKEN_KEY, user.token);
          }
        })
      );
  }

  registerRenter(renterData: RenterRegistrationRequest) {
    return this.http
      .post<User>(`${this.apiUrl}/Authentication/register/renter`, renterData)
      .pipe(
        tap((user) => {
          console.log('Renter registered successfully:', user);
          if (isPlatformBrowser(this.platformId)) {
            localStorage.setItem(this.USER_KEY, JSON.stringify(user));
            localStorage.setItem(this.TOKEN_KEY, user.token);
          }
        })
      );
  }

  registerDealer(dealerData: DealerRegistrationRequest) {
    return this.http
      .post<User>(`${this.apiUrl}/Authentication/register/dealer`, dealerData)
      .pipe(
        tap((user) => {
          console.log('Dealer registered successfully:', user);
          if (isPlatformBrowser(this.platformId)) {
            localStorage.setItem(this.USER_KEY, JSON.stringify(user));
            localStorage.setItem(this.TOKEN_KEY, user.token);
          }
        })
      );
  }
  

  logout() {
    const token = localStorage.getItem(this.TOKEN_KEY);
  
    if (!token) {
      console.error('No token found in localStorage');
      return this.http.get(`${this.apiUrl}/Authentication/logout`);
    }
  
    const headers = new HttpHeaders().set('Token', token);

    return this.http
      .get(`${this.apiUrl}/Authentication/logout`, { headers })
      .pipe(
        tap(() => {
          this.user$$.next(null);
          if (isPlatformBrowser(this.platformId)) {
            localStorage.removeItem(this.USER_KEY);
            localStorage.removeItem(this.TOKEN_KEY);
          }
        }),
        catchError(error => {
          console.error('Logout failed', error);
          throw error;
        })
      );
  }
  
  ngOnDestroy(): void {
    this.userSubscription?.unsubscribe();
  }
}
