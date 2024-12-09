import { ComponentFixture, TestBed } from '@angular/core/testing';
import { MyCarsComponent } from './mycars.component';
import { CarService } from '../car.service';
import { UserService } from '../../user/user.service';
import { of, throwError } from 'rxjs';
import { CommonModule } from '@angular/common';
import { LoaderComponent } from '../../shared/loader/loader.component';
import { Car } from '../../types/car';

describe('MyCarsComponent', () => {
  let component: MyCarsComponent;
  let fixture: ComponentFixture<MyCarsComponent>;
  let carServiceMock: any;
  let userServiceMock: any;

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
  ]
  
  beforeEach(async () => {
    carServiceMock = {
      getCarsByDealerId: jasmine.createSpy('getCarsByDealerId').and.returnValue(of(mockCars)),
    };

    userServiceMock = {
      userObservable: of({ user: { dealerId: 1 } }),
    };

    await TestBed.configureTestingModule({
      declarations: [MyCarsComponent],
      imports: [CommonModule, LoaderComponent],
      providers: [
        { provide: CarService, useValue: carServiceMock },
        { provide: UserService, useValue: userServiceMock },
      ],
    }).compileComponents();

    fixture = TestBed.createComponent(MyCarsComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create the component', () => {
    expect(component).toBeTruthy();
  });

  it('should load cars on initialization if dealerId is available', () => {
    expect(userServiceMock.userObservable).toBeTruthy();
    expect(carServiceMock.getCarsByDealerId).toHaveBeenCalledWith(1);
    expect(component.cars).toEqual(mockCars);
    expect(component.isLoading).toBeFalse();
  });

  it('should handle error when loading cars fails', () => {
    carServiceMock.getCarsByDealerId.and.returnValue(throwError('Error fetching cars'));
    component.loadCars();

    expect(carServiceMock.getCarsByDealerId).toHaveBeenCalledWith(1);
    expect(component.cars).toEqual([]);
    expect(component.errorMessage).toBe('Failed to fetch cars. Please try again later.');
    expect(component.isLoading).toBeFalse();
  });

  it('should show error message if dealerId is not available', () => {
    userServiceMock.userObservable = of(null);
    component.ngOnInit();

    expect(component.dealerId).toBeNull();
    expect(component.errorMessage).toBe('You must be logged in as a dealer to view your cars.');
    expect(component.isLoading).toBeFalse();
  });

  it('should display the correct number of cars', () => {
    fixture.detectChanges();
    const compiled = fixture.nativeElement as HTMLElement;
    const carRows = compiled.querySelectorAll('tbody tr');

    expect(carRows.length).toBe(mockCars.length);
  });

  it('should show a message when there are no cars', () => {
    carServiceMock.getCarsByDealerId.and.returnValue(of([]));
    component.loadCars();

    fixture.detectChanges();
    const compiled = fixture.nativeElement as HTMLElement;
    const noCarsMessage = compiled.querySelector('.no-cars p')?.textContent;

    expect(component.cars.length).toBe(0);
    expect(noCarsMessage).toBe('You have no cars listed.');
  });

  it('should retrieve dealerId from local storage if not in user data', () => {
    spyOn(localStorage, 'getItem').and.returnValue(JSON.stringify({ dealerId: 2 }));
    userServiceMock.userObservable = of({ user: null });
    component.ngOnInit();

    expect(component.dealerId).toBe(2);
  });
});
