import { Component, OnDestroy, OnInit } from '@angular/core';
import { ActivatedRoute, RouterLink } from '@angular/router';
import { Car } from '../../types/car';
import { CarService } from '../car.service';
import { Subscription } from 'rxjs';
import { UserService } from '../../user/user.service';
import { CommonModule } from '@angular/common';
import { User } from '../../types/user';
import { LoaderComponent } from '../../shared/loader/loader.component';
import { AverageRatingPipe } from '../../pipes/average.rating.pipe';


@Component({
  selector: 'app-car-details',
  standalone: true,
  imports: [RouterLink, CommonModule, LoaderComponent],
  templateUrl: './car-details.component.html',
  styleUrl: './car-details.component.css'
})
export class CarDetailComponent implements OnInit, OnDestroy {
  car: Car | null = null;
  user: User | null = null;  
  userSubscription: Subscription | null = null;
  loading: boolean = true;
  ratings: number[] = []; 
  averageRating: number = 0; 
  stars: number[] = [1, 2, 3, 4, 5]

  constructor(
    private route: ActivatedRoute,    
    private carService: CarService,   
    private userService: UserService  
  ) {}

  ngOnInit(): void {
    this.userService.userObservable.subscribe((user) => {
      this.user = user;
    });
    
    const carId = this.route.snapshot.paramMap.get('id');
    if (carId) {
      this.carService.getCarById(Number(carId)).subscribe(
        (data: Car) => {
          this.car = data;
          this.loading = false;
        },
        (error) => {
          console.error('Error fetching car details:', error);
          this.loading = false;  
        }
      );
    }

    this.carService.getRatingsByCarId(Number(carId)).subscribe(
      (ratings: number[]) => {
        this.ratings = ratings;
        console.log(ratings)
        this.averageRating = new AverageRatingPipe().transform(ratings); 
        console.log(this.averageRating)
      },
      (error) => {
        console.error('Error fetching ratings:', error);
      }
    );
  
    this.userSubscription = this.userService.userObservable.subscribe(
      (user) => {
        this.user = user; 
      }
    );
  }

  ngOnDestroy(): void {
    if (this.userSubscription) {
      this.userSubscription.unsubscribe();
    }
  }
}