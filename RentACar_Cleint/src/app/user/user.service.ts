import { Injectable, OnDestroy, Inject, PLATFORM_ID } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { BehaviorSubject, catchError, Observable, Subscription, tap } from 'rxjs';
import { User } from '../types/user';
import { isPlatformBrowser } from '@angular/common';
import { DealerRegistrationRequest } from '../types/dealer-registration-request';
import { RenterRegistrationRequest } from '../types/renter-registration-request';

@Injectable({
  providedIn: 'root',
})
export class UserService implements OnDestroy {
  private user$$ = new BehaviorSubject<User | null>(null);
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
  get user$(): Observable<User | null> {
    return this.user$$.asObservable();
  }

  constructor(private http: HttpClient, @Inject(PLATFORM_ID) private platformId: Object) {
    this.userSubscription = this.user$.subscribe((user) => {
      this.user = user;
      if (user) {
        if (isPlatformBrowser(this.platformId)) {
          localStorage.setItem(this.TOKEN_KEY, user.token);
          localStorage.setItem(this.USER_KEY, JSON.stringify(user));
        }
      } 
    });

    this.loadUserFromLocalStorage();
  }

  public loadUserFromLocalStorage() {
    if (isPlatformBrowser(this.platformId)) {
      const savedUser = localStorage.getItem(this.USER_KEY);
      const savedToken = localStorage.getItem(this.TOKEN_KEY);
  
      if (savedUser && savedToken) {
        const user = JSON.parse(savedUser);
  
        if (this.isTokenValid(savedToken)) {
          this.user$$.next(user);
        } else {
          console.warn('Invalid token. Clearing storage.');
          this.clearStorage();
        }
      } else {
        console.warn('No user or token found. Setting user to null.');
        this.user$$.next(null);
      }
    }
  }

  private clearStorage() {
    this.user$$.next(null);
    localStorage.removeItem(this.USER_KEY);
    localStorage.removeItem(this.TOKEN_KEY);
  }

  private isTokenValid(token: string): boolean {
    const decodedToken = this.decodeToken(token);
    const currentTime = Math.floor(Date.now() / 1000);
    return decodedToken?.exp > currentTime; 
  }
  
  private decodeToken(token: string): any {
    const parts = token.split('.');
    if (parts.length === 3) {
      const payload = parts[1];
      return JSON.parse(atob(payload));  
    }
    return null;
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
