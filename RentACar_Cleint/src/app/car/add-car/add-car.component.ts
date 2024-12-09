import { CommonModule } from '@angular/common';
import { Component, OnInit } from '@angular/core';
import { User } from '../../types/user';
import { CarService } from '../car.service';
import { UserService } from '../../user/user.service';

import { FormsModule, NgForm } from '@angular/forms';
import { Router } from '@angular/router';
import { LoaderComponent } from '../../shared/loader/loader.component';
import { Category } from '../../types/category';

@Component({
  selector: 'app-add-car',
  standalone: true,
  imports: [CommonModule, FormsModule, LoaderComponent],
  templateUrl: './add-car.component.html',
  styleUrl: './add-car.component.css'
})
export class AddCarComponent implements OnInit {
  formValues = {
    regNumber: '',
    model: '',
    make: '',
    makeYear: 0,
    airCondition: false,
    seats: 0,
    doors: 0,
    navigationSystem: false,
    fuel: '',
    transmission: '',
    dailyRate: 0,
    imageUrl: '',
    categoryId: 0,
    dealerId: 0,
  };
  isLoading = false;
  dealerId: number | null = null;
  user: User | null = null;
  categories: Category[] = []; 

  constructor(
    private carService: CarService,
    private userService: UserService,
    private router: Router,
  ) {}

  ngOnInit(): void {
    this.userService.userObservable.subscribe((user) => {
      this.user = user;
      if(user){
        this.dealerId = user?.user.dealerId
      }
    });
    this.carService.getCategories().subscribe(
      (categories) => {
        this.categories = categories;
      },
      (error) => {
        console.error('Error fetching categories:', error);
      }
    );
  }

  onFileChange(event: Event): void {
    const input = event.target as HTMLInputElement;
    if (input && input.files) {
      const file = input.files[0];
      this.uploadImage(file);
    }
  }

  uploadImage(file: File): void {
    const url = 'https://localhost:7016/api/BlobStorage';
    const formData = new FormData();
    formData.append('imageFile', file);
  
    const options = {
      method: 'POST',
      body: formData,
    };
    const fileName = file.name;
    this.formValues.imageUrl = fileName;
  
    fetch(url, options)
      .then((response) => response.json())
      .then(() => {
       console.log('Image successfully uploaded')
      })
      .catch((error) => {
        console.error('Error during image upload:', error);
      });
  }

  submitCar(form: NgForm): void {
    if (form.invalid) {
      console.error('Form is invalid!');
      return;
    }
    const carData = { ...this.formValues, dealerId: this.dealerId };
    this.isLoading = true;  

    this.carService.createCar(carData).subscribe(
      (response) => {
        this.isLoading = false;  
        this.router.navigate(['/my-cars']);  
      },
      (error) => {
        this.isLoading = false;  
        console.error('Adding car failed:', error);
      }
    );
  }
}
