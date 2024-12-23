import { Component, OnInit } from '@angular/core';
import { RouterLink } from '@angular/router';
import { CommonModule } from '@angular/common';
import { CarService } from '../car.service';
import { Car } from '../../types/car';
import { FormsModule } from '@angular/forms';
import { LoaderComponent } from '../../shared/loader/loader.component';

@Component({
  selector: 'app-catalog',
  standalone: true,
  imports: [CommonModule, FormsModule, RouterLink, LoaderComponent],
  templateUrl: './catalog.component.html',
  styleUrls: ['./catalog.component.css']
})
export class CatalogComponent implements OnInit {
  cars: Car[] = []; 
  filteredCars: Car[] = []; 
  displayedCars: Car[] = []; 
  searchQuery: string = ''; 
  currentPage: number = 1; 
  itemsPerPage: number = 8; 
  isLoading: boolean = false; 

  constructor(private carService: CarService) {}

  ngOnInit(): void {
    this.loadCars();
  }

  loadCars(): void {
    this.isLoading = true;
    this.carService.getAllCars().subscribe({
      next: (data) => {
        this.cars = data; 
        this.filteredCars = [...this.cars]; 
        this.updateDisplayedCars(); 
        this.isLoading = false;
      },
      error: (err) => {
        console.error('Error loading cars:', err);
        this.isLoading = false;  
      }
    });
  }

  onSearch(query: string): void {
    this.searchQuery = query.toLowerCase();
    this.currentPage = 1; 
    this.filteredCars = this.cars.filter((car) =>
      `${car.make} ${car.model} ${car.categoryName}`.toLowerCase().includes(this.searchQuery)
    );
    this.updateDisplayedCars();
  }

  updateDisplayedCars(): void {
    const startIndex = (this.currentPage - 1) * this.itemsPerPage;
    this.displayedCars = this.filteredCars.slice(startIndex, startIndex + this.itemsPerPage);
  }

  onPageChange(newPage: number): void {
    this.currentPage = newPage;
    this.updateDisplayedCars();
  }

  get totalPages(): number {
    return Math.ceil(this.filteredCars.length / this.itemsPerPage);
  }
}