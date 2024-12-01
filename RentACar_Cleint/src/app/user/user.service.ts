import { Injectable, OnDestroy, Inject, PLATFORM_ID } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { BehaviorSubject, Subscription, tap } from 'rxjs';
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
      .post<User>('https://localhost:7016/api/Authentication/login', { userName, password })
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
      .post<User>(`https://localhost:7016/api/Authentication/register/renter`, renterData)
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
      .post<User>(`https://localhost:7016/api/Authentication/register/dealer`, dealerData)
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
    return this.http
      .get('https://localhost:7016/api/Authentication/logout', { headers: { Authorization: `Bearer ${token}` } })
      .pipe(
        tap(() => {
          this.user$$.next(null);
          if (isPlatformBrowser(this.platformId)) {
            localStorage.removeItem(this.USER_KEY);
            localStorage.removeItem(this.TOKEN_KEY);
          }
        })
      );
  }

  ngOnDestroy(): void {
    this.userSubscription?.unsubscribe();
  }
}
