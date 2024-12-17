import { Component, EventEmitter, Input, Output } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { Category } from '../../types/category';
import { CarService } from '../car.service';
import { CarForm } from '../../types/car-form';
import { Car } from '../../types/car';

@Component({
  selector: 'app-edit-car',
  standalone: true,
  imports: [FormsModule],
  templateUrl: './edit-car.component.html',
  styleUrl: './edit-car.component.css'
})
export class EditCarComponent {
  @Input() car: Car | null = null; 
  @Output() save = new EventEmitter<Car>(); 
  @Output() cancel = new EventEmitter<void>();
  isSaving: boolean = false; 
  categories: Category[] = []; 

  constructor(
    private carService: CarService,
  ) {}

  ngOnInit(): void {
    this.carService.getCategories().subscribe(
      (categories) => {
        this.categories = categories;
      },
      (error) => {
        console.error('Error fetching categories:', error);
      }
    );
  }

  onSave(): void {
    if (this.car) {
      console.log(this.car)
      this.isSaving = true; 
      this.save.emit(this.car);
      this.isSaving = false;
    }
  }
  
  onCancel(): void {
      this.cancel.emit(); 
  }
}
