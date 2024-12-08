import { Injectable } from '@angular/core';
import { Car } from '../types/car'; 
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs/internal/Observable';
import { map } from 'rxjs/operators';
import { CarForm } from '../types/car-form';
import { Category } from '../types/category';

@Injectable({
  providedIn: 'root',
})
export class CarService {

  private apiUrl = 'https://localhost:7016/api';

  constructor(private http: HttpClient) {}

  getAllCars(): Observable<Car[]> {
    return this.http.get<Car[]>(`${this.apiUrl}/Car`);
  }

  getCarById(id: number): Observable<Car> {
    return this.http.get<Car>(`${this.apiUrl}/Car/${id}`);
  }

  getCarsByDealerId(dealerId: number): Observable<Car[]> {
    return this.http.get<Car[]>(`${this.apiUrl}/Car/Dealer/${dealerId}`);
  }

  getRatingsByCarId(carId: number): Observable<number[]> {
    return this.http.get<{ rate: number }[]>(`${this.apiUrl}/Rating/Car/${carId}`)
      .pipe(
        map((ratings) => ratings.map(rating => rating.rate)) 
      );
  }

  createCar(carData: CarForm): Observable<any> {
    return this.http.post(`${this.apiUrl}/Car`, carData);
  }

  getCategories(): Observable<Category[]> {
    return this.http.get<Category[]>(`${this.apiUrl}/Category`);
  }
}
