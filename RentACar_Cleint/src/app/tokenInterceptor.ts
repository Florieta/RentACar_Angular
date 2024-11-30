import {
    Injectable
  } from '@angular/core';
  import {
    HttpInterceptor,
    HttpRequest,
    HttpHandler,
    HttpEvent,
    HttpErrorResponse,
    HttpResponse
  } from '@angular/common/http';
  import { Observable, throwError } from 'rxjs';
  import { tap, catchError } from 'rxjs/operators';
  import { Router } from '@angular/router';
  import { AuthService } from './auth.service'; 
  
  @Injectable()
  export class TokenInterceptor implements HttpInterceptor {
    constructor(private authService: AuthService, private router: Router) {}
  
    intercept(request: HttpRequest<any>, next: HttpHandler): Observable<HttpEvent<any>> {
      const token = this.authService.getToken();
      if (token) {
        request = request.clone({
          setHeaders: {
            Authorization: `Bearer ${token}`,
            'Content-Type': 'application/json'
          }
        });
      }
  
      return next.handle(request).pipe(
        tap((event: HttpEvent<any>) => {
          if (event instanceof HttpResponse) {
            if (request.url.endsWith('/login') && event.body?.token) {
              this.authService.saveToken(event.body.token);
            }
          }
        }),
        catchError((error: HttpErrorResponse) => {
          if (error.status === 401) {
            console.error('Unauthorized request - redirecting to login.');
            this.authService.logout(); 
            this.router.navigate(['/login']); 
          } else if (error.status === 403) {
            console.error('Forbidden request - insufficient permissions.');
            this.router.navigate(['/forbidden']);
          } else {
            console.error(`HTTP Error: ${error.status} - ${error.message}`);
          }
          return throwError(error);
        })
      );
    }
  }
  