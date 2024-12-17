import { Component, OnInit } from '@angular/core';
import { Car } from '../../types/car';
import { CarService } from '../car.service';
import { UserService } from '../../user/user.service';
import { CommonModule } from '@angular/common';
import { LoaderComponent } from '../../shared/loader/loader.component';
import { EditCarComponent } from '../edit-car/edit-car.component';

@Component({
    selector: 'app-mycars',
    standalone: true,
    imports: [CommonModule, LoaderComponent, EditCarComponent],
    templateUrl: './mycars.component.html',
    styleUrls: ['./mycars.component.css']
})
export class MyCarsComponent implements OnInit {
    cars: Car[] = [];
    dealerId: number | null = null;
    errorMessage: string = '';
    isLoading: boolean = true;
    editingCar: Car | null = null;

    constructor(
        private carService: CarService,
        private userService: UserService,
    ) { }

    ngOnInit(): void {
        this.userService.userObservable.subscribe((user) => {
            if (user) {
              this.dealerId = user.user.dealerId || this.getDealerIdFromLocalStorage();
              if (this.dealerId) {
                this.loadCars();
              } else {
                this.errorMessage = 'You must be logged in as a dealer to view your cars.';
                this.isLoading = false;
              }
            } else {
              this.errorMessage = 'You must be logged in as a dealer to view your cars.';
              this.isLoading = false;
            }
          });
        }

    loadCars(): void {
        if (this.dealerId === null) return;

        this.carService.getCarsByDealerId(this.dealerId).subscribe({
            next: (data: Car[]) => {
                this.cars = data;
                this.isLoading = false;
            },
            error: (err) => {
                this.errorMessage = 'Failed to fetch cars. Please try again later.';
                this.isLoading = false;
                console.error('Error loading cars:', err);
            }
        });
    }

    enableEdit(car: Car): void {
        this.editingCar = { ...car, imageUrl: car.imageUrl, dealerId: this.dealerId };
    }
    
    cancelEdit(): void {
        this.editingCar = null; 
    }
    
    saveEdit(updatedCar: Car): void {
        this.carService.editCar(updatedCar).subscribe({
            next: () => {
                this.editingCar = null; 
                this.loadCars(); 
            },
            error: (err) => {
                console.error('Failed to update car:', err);
            }
        });
    }
  
    deleteCar(carId: number): void {
        if (confirm('Are you sure you want to delete this car?')) {
            this.carService.deleteCar(carId).subscribe({
                next: () => {
                    this.cars = this.cars.filter(car => car.id !== carId);
                    alert('Car deleted successfully');
                },
                error: (err) => {
                    this.errorMessage = 'Failed to delete the car. Please try again later.';
                    console.error('Error deleting car:', err);
                }
            });
        }
    }
    
    private getDealerIdFromLocalStorage(): number | null {
        const storedUser = localStorage.getItem('user');
        if (storedUser) {
            const parsedUser = JSON.parse(storedUser);
            return parsedUser.dealerId || null;
        }
        return null;
    }
}
