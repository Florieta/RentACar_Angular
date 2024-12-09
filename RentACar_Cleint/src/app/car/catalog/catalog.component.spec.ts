import { ComponentFixture, TestBed } from '@angular/core/testing';
import { CatalogComponent } from './catalog.component';
import { CarService } from '../car.service';
import { of, throwError } from 'rxjs';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { RouterLink } from '@angular/router';
import { LoaderComponent } from '../../shared/loader/loader.component';
import { Car } from '../../types/car';

describe('CatalogComponent', () => {
  let component: CatalogComponent;
  let fixture: ComponentFixture<CatalogComponent>;
  let carServiceMock: any;

  const mockCars: Car[] = [
    {
      id: 1,
      make: 'Toyota',
      model: 'Camry',
      categoryName: 'Sedan',
      dailyRate: 50,
      imageUrl: 'car1.jpg',
      regNumber: 'XYZ123',
      makeYear: 2020,
      airCondition: true,
      seats: 5,
      doors: 4,
      fuel: 'Petrol',
      transmission: 'Automatic',
      navigationSystem: true,
      categoryId: 1,
      isAvailable: true
    },
    {
      id: 2,
      make: 'Honda',
      model: 'Civic',
      categoryName: 'Sedan',
      dailyRate: 45,
      imageUrl: 'car2.jpg',
      regNumber: 'ABC987',
      makeYear: 2019,
      airCondition: true,
      seats: 5,
      doors: 4,
      fuel: 'Petrol',
      transmission: 'Manual',
      navigationSystem: false,
      categoryId: 2,
      isAvailable: true
    },
    {
      id: 3,
      make: 'Tesla',
      model: 'Model S',
      categoryName: 'Electric',
      dailyRate: 120,
      imageUrl: 'car3.jpg',
      regNumber: 'TES123',
      makeYear: 2022,
      airCondition: true,
      seats: 5,
      doors: 4,
      fuel: 'Electric',
      transmission: 'Automatic',
      navigationSystem: true,
      categoryId: 3,
      isAvailable: true
    },
  ];
  
  beforeEach(async () => {
    carServiceMock = {
      getAllCars: jasmine.createSpy('getAllCars').and.returnValue(of(mockCars)),
    };

    await TestBed.configureTestingModule({
      declarations: [CatalogComponent],
      imports: [CommonModule, FormsModule, RouterLink, LoaderComponent],
      providers: [
        { provide: CarService, useValue: carServiceMock },
      ],
    }).compileComponents();

    fixture = TestBed.createComponent(CatalogComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create the component', () => {
    expect(component).toBeTruthy();
  });

  it('should load cars on initialization', () => {
    expect(carServiceMock.getAllCars).toHaveBeenCalled();
    expect(component.cars).toEqual(mockCars);
    expect(component.filteredCars).toEqual(mockCars);
    expect(component.displayedCars.length).toBeLessThanOrEqual(component.itemsPerPage);
    expect(component.isLoading).toBeFalse();
  });

  it('should handle error when loading cars fails', () => {
    carServiceMock.getAllCars.and.returnValue(throwError('Error fetching cars'));
    component.loadCars();
    expect(carServiceMock.getAllCars).toHaveBeenCalled();
    expect(component.cars).toEqual([]);
    expect(component.filteredCars).toEqual([]);
    expect(component.isLoading).toBeFalse();
    expect(console.error).toHaveBeenCalledWith('Error loading cars:', 'Error fetching cars');
  });

  it('should filter cars based on search query', () => {
    component.onSearch('Tesla');
    expect(component.filteredCars).toEqual([mockCars[2]]);
    expect(component.currentPage).toBe(1);
    expect(component.displayedCars).toEqual([mockCars[2]]);
  });

  it('should display correct cars for the current page', () => {
    component.itemsPerPage = 1;
    component.updateDisplayedCars();

    component.currentPage = 1;
    component.updateDisplayedCars();
    expect(component.displayedCars).toEqual([mockCars[0]]);

    component.currentPage = 2;
    component.updateDisplayedCars();
    expect(component.displayedCars).toEqual([mockCars[1]]);
  });

  it('should handle pagination correctly', () => {
    component.itemsPerPage = 1;
    component.onPageChange(2);
    expect(component.currentPage).toBe(2);
    expect(component.displayedCars).toEqual([mockCars[1]]);

    component.onPageChange(1);
    expect(component.currentPage).toBe(1);
    expect(component.displayedCars).toEqual([mockCars[0]]);
  });

  it('should calculate total pages correctly', () => {
    component.itemsPerPage = 2;
    expect(component.totalPages).toBe(2);

    component.itemsPerPage = 3;
    expect(component.totalPages).toBe(1);
  });
});