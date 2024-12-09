import { ComponentFixture, TestBed } from '@angular/core/testing';
import { CarDetailComponent } from './car-details.component';
import { CarService } from '../car.service';
import { UserService } from '../../user/user.service';
import { ActivatedRoute } from '@angular/router';
import { of, throwError } from 'rxjs';
import { CommonModule } from '@angular/common';
import { LoaderComponent } from '../../shared/loader/loader.component';
import { AverageRatingPipe } from '../../pipes/average.rating.pipe';

describe('CarDetailComponent', () => {
  let component: CarDetailComponent;
  let fixture: ComponentFixture<CarDetailComponent>;
  let carServiceMock: any;
  let userServiceMock: any;
  let activatedRouteMock: any;

  beforeEach(async () => {
    carServiceMock = {
      getCarById: jasmine.createSpy('getCarById').and.returnValue(
        of({
          id: 1,
          make: 'Toyota',
          model: 'Camry',
          makeYear: 2022,
          dailyRate: 50,
          imageUrl: 'image.jpg',
          categoryName: 'Sedan',
          doors: 4,
          seats: 5,
          fuel: 'Petrol',
          transmission: 'Automatic',
          airCondition: true,
          navigationSystem: true,
          regNumber: 'XYZ123',
        })
      ),
      getRatingsByCarId: jasmine.createSpy('getRatingsByCarId').and.returnValue(of([5, 4, 4, 5])),
    };

    userServiceMock = {
      userObservable: of({ user: { token: 'abc123', renterId: 42 } }),
    };

    activatedRouteMock = {
      snapshot: {
        paramMap: {
          get: jasmine.createSpy('get').and.returnValue('1'),
        },
      },
    };

    await TestBed.configureTestingModule({
      declarations: [AverageRatingPipe, CarDetailComponent],
      imports: [CommonModule, LoaderComponent],
      providers: [
        { provide: CarService, useValue: carServiceMock },
        { provide: UserService, useValue: userServiceMock },
        { provide: ActivatedRoute, useValue: activatedRouteMock },
      ],
    }).compileComponents();

    fixture = TestBed.createComponent(CarDetailComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create the component', () => {
    expect(component).toBeTruthy();
  });

  it('should fetch car details and ratings on initialization', () => {
    expect(carServiceMock.getCarById).toHaveBeenCalledWith(1);
    expect(component.car).toEqual(jasmine.objectContaining({ make: 'Toyota', model: 'Camry' }));
    expect(component.loading).toBeFalse();

    expect(carServiceMock.getRatingsByCarId).toHaveBeenCalledWith(1);
    expect(component.ratings).toEqual([5, 4, 4, 5]);
    expect(component.averageRating).toEqual(4.5);
  });

  it('should handle error when fetching car details fails', () => {
    carServiceMock.getCarById.and.returnValue(throwError('Error fetching car details'));
    component.ngOnInit();

    expect(carServiceMock.getCarById).toHaveBeenCalled();
    expect(component.car).toBeNull();
    expect(component.loading).toBeFalse();
    expect(console.error).toHaveBeenCalledWith('Error fetching car details:', 'Error fetching car details');
  });

  it('should handle error when fetching ratings fails', () => {
    carServiceMock.getRatingsByCarId.and.returnValue(throwError('Error fetching ratings'));
    component.ngOnInit();

    expect(carServiceMock.getRatingsByCarId).toHaveBeenCalled();
    expect(component.ratings).toEqual([]);
    expect(console.error).toHaveBeenCalledWith('Error fetching ratings:', 'Error fetching ratings');
  });

  it('should unsubscribe from user observable on destroy', () => {
    const userSubscriptionSpy = spyOn(component.userSubscription!, 'unsubscribe');
    component.ngOnDestroy();
    expect(userSubscriptionSpy).toHaveBeenCalled();
  });

  it('should render car details correctly', () => {
    fixture.detectChanges();
    const compiled = fixture.nativeElement as HTMLElement;

    expect(compiled.querySelector('h2')?.textContent).toContain('Toyota Camry (2022)');
    expect(compiled.querySelector('.daily-rate')?.textContent).toContain('50€ per day');
    expect(compiled.querySelector('img')?.getAttribute('src')).toBe('image.jpg');
  });

  it('should render average rating as stars', () => {
    fixture.detectChanges();
    const compiled = fixture.nativeElement as HTMLElement;
    const filledStars = compiled.querySelectorAll('.stars .filled');

    expect(filledStars.length).toBe(4); 
  });
});
