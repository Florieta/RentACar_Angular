import { Injectable } from '@angular/core';
import { Car } from './types/car'; 
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs/internal/Observable';

@Injectable({
  providedIn: 'root',
})
export class CarService {

  private apiUrl = 'https://localhost:7016/api/Car/Latest';

  constructor(private http: HttpClient) {}

  getAllCars(): Observable<Car[]> {
    return this.http.get<Car[]>(this.apiUrl);
  }

  getCarById(id: number): Observable<Car> {
    return this.http.get<Car>(`https://localhost:7016/api/Car/${id}`);
  }
}
