import { ComponentFixture, TestBed } from '@angular/core/testing';
import { FormsModule } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { of } from 'rxjs';
import { BookingComponent } from './create-booking.component';
import { BookingService } from '../booking.service';
import { UserService } from '../../user/user.service';
import { CarLocation } from '../../types/location';

describe('BookingComponent', () => {
  let component: BookingComponent;
  let fixture: ComponentFixture<BookingComponent>;
  let bookingServiceMock: any;
  let userServiceMock: any;
  let routerMock: any;
  let activatedRouteMock: any;

  beforeEach(async () => {
    bookingServiceMock = {
      getLocations: jasmine.createSpy().and.returnValue(of([{ id: 1, locationName: 'Location 1' }] as CarLocation[])),
      createBooking: jasmine.createSpy().and.returnValue(of({ id: 1 })),
    };

    userServiceMock = {
      userObservable: of({ user: { renterId: 123 } }),
    };

    routerMock = {
      navigate: jasmine.createSpy(),
    };

    activatedRouteMock = {
      queryParams: of({ carId: '1', dailyRate: '100' }),
    };

    await TestBed.configureTestingModule({
      imports: [FormsModule],
      declarations: [BookingComponent],
      providers: [
        { provide: BookingService, useValue: bookingServiceMock },
        { provide: UserService, useValue: userServiceMock },
        { provide: Router, useValue: routerMock },
        { provide: ActivatedRoute, useValue: activatedRouteMock },
      ],
    }).compileComponents();

    fixture = TestBed.createComponent(BookingComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create the component', () => {
    expect(component).toBeTruthy();
  });

  it('should initialize with user and query parameters', () => {
    expect(component.renterId).toBe(123);
    expect(component.carId).toBe(1);
    expect(component.dailyRate).toBe(100);
    expect(component.formValues.carId).toBe(1);
    expect(component.formValues.dailyRate).toBe(100);
  });

  it('should load locations on init', () => {
    expect(bookingServiceMock.getLocations).toHaveBeenCalled();
    expect(component.locations.length).toBe(1);
    expect(component.locations[0].locationName).toBe('Location 1');
  });

  it('should calculate the total amount correctly', () => {
    component.formValues.duration = 3;
    component.formValues.dailyRate = 100;
    component.calculateTotalAmount();
    expect(component.formValues.totalAmount).toBe(300);
  });

  it('should call createBooking and navigate on successful submission', () => {
    const mockForm = {
      invalid: false,
    } as any;

    component.submitBooking(mockForm);

    expect(component.isLoading).toBe(true);
    expect(bookingServiceMock.createBooking).toHaveBeenCalledWith({
      ...component.formValues,
      renterId: 123,
    });

    fixture.detectChanges();

    expect(component.isLoading).toBe(false);
    expect(routerMock.navigate).toHaveBeenCalledWith(['/my-bookings']);
  });

  it('should handle form validation errors', () => {
    const mockForm = {
      invalid: true,
    } as any;

    spyOn(console, 'error');

    component.submitBooking(mockForm);

    expect(console.error).toHaveBeenCalledWith('Form is invalid!');
    expect(bookingServiceMock.createBooking).not.toHaveBeenCalled();
  });
});
