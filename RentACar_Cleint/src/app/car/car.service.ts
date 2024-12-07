import { Injectable } from '@angular/core';
import { Car } from '../types/car'; 
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs/internal/Observable';
import { Rating } from '../types/rating';
import { map } from 'rxjs/operators';

@Injectable({
  providedIn: 'root',
})
export class CarService {

  private apiUrl = 'https://localhost:7016/api/Car';

  constructor(private http: HttpClient) {}

  getAllCars(): Observable<Car[]> {
    return this.http.get<Car[]>(this.apiUrl);
  }

  getCarById(id: number): Observable<Car> {
    return this.http.get<Car>(`https://localhost:7016/api/Car/${id}`);
  }

  getCarsByDealerId(dealerId: number): Observable<Car[]> {
    return this.http.get<Car[]>(`https://localhost:7016/api/Car/Dealer/${dealerId}`);
  }

  getRatingsByCarId(carId: number): Observable<number[]> {
    return this.http.get<{ rate: number }[]>(`https://localhost:7016/api/Rating/Car/${carId}`)
      .pipe(
        map((ratings) => ratings.map(rating => rating.rate)) 
      );
  }
}
