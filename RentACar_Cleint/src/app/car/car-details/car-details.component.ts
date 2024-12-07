import { Component, OnDestroy, OnInit } from '@angular/core';
import { ActivatedRoute, RouterLink } from '@angular/router';
import { Car } from '../../types/car';
import { CarService } from '../car.service';
import { Subscription } from 'rxjs';
import { UserService } from '../../user/user.service';
import { CommonModule } from '@angular/common';
import { User } from '../../types/user';
import { LoaderComponent } from '../../shared/loader/loader.component';

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