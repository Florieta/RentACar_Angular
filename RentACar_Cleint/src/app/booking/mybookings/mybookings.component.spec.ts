import { ComponentFixture, TestBed } from '@angular/core/testing';
import { of, throwError } from 'rxjs';
import { MyBookingsComponent } from './mybookings.component';
import { BookingService } from '../booking.service';
import { UserService } from '../../user/user.service';
import { Booking } from '../../types/booking';
import { CommonModule } from '@angular/common';
import { LoaderComponent } from '../../shared/loader/loader.component';

describe('MyBookingsComponent', () => {
  let component: MyBookingsComponent;
  let fixture: ComponentFixture<MyBookingsComponent>;
  let bookingServiceMock: any;
  let userServiceMock: any;

  const mockBookings: Booking[] = [
    {
      id: 1,
    carMake: 'Toyota',
    carModel: 'Camry',
    regNumber: 'XYZ123',
    pickUpDateAndTime: new Date('2023-01-01T10:00:00'),
    dropOffDateAndTime: new Date('2023-01-03T10:00:00'),
    duration: 2, 
    isActive: true,
    totalAmount: 150, 
    paymentType: 'Credit Card', 
    isPaid: true, 
    pickUpLocation: 'Downtown Office', 
    dropOffLocation: 'Airport Office', 
    renterId: 1
  },
  {
    id: 2,
    carMake: 'Honda',
    carModel: 'Civic',
    regNumber: 'ABC987',
    pickUpDateAndTime: new Date('2023-01-05T12:00:00'),
    dropOffDateAndTime: new Date('2023-01-07T12:00:00'),
    duration: 2, 
    isActive: false,
    totalAmount: 120, 
    paymentType: 'PayPal', 
    isPaid: false, 
    pickUpLocation: 'Airport Office', 
    dropOffLocation: 'Downtown Office', 
    renterId: 1
    },
  ];

  beforeEach(async () => {
    bookingServiceMock = {
      getBookingsByRenterId: jasmine.createSpy('getBookingsByRenterId').and.returnValue(of(mockBookings)),
      cancelBooking: jasmine.createSpy('cancelBooking').and.returnValue(of({})),
    };

    userServiceMock = {
      userObservable: of({ user: { renterId: 1 } }),
    };

    await TestBed.configureTestingModule({
      declarations: [MyBookingsComponent],
      imports: [CommonModule, LoaderComponent],
      providers: [
        { provide: BookingService, useValue: bookingServiceMock },
        { provide: UserService, useValue: userServiceMock },
      ],
    }).compileComponents();

    fixture = TestBed.createComponent(MyBookingsComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create the component', () => {
    expect(component).toBeTruthy();
  });

  it('should load bookings on init if renterId is available', () => {
    expect(bookingServiceMock.getBookingsByRenterId).toHaveBeenCalledWith(1);
    expect(component.bookings).toEqual(mockBookings);
    expect(component.isLoading).toBeFalse();
  });

  it('should show an error message if renterId is not available', () => {
    userServiceMock.userObservable = of(null);
    component.ngOnInit();
    expect(component.errorMessage).toBe('You must be logged in as a renter to view your bookings.');
    expect(component.isLoading).toBeFalse();
  });

  it('should handle error when loading bookings fails', () => {
    bookingServiceMock.getBookingsByRenterId.and.returnValue(throwError('Error fetching bookings'));
    component.loadBookings();
    expect(component.errorMessage).toBe('Failed to load bookings. Please try again later.');
    expect(component.isLoading).toBeFalse();
  });

  it('should call cancelBooking and reload bookings on success', () => {
    spyOn(window, 'confirm').and.returnValue(true);
    component.cancelBooking(1);
    expect(bookingServiceMock.cancelBooking).toHaveBeenCalledWith(1);
    expect(bookingServiceMock.getBookingsByRenterId).toHaveBeenCalled();
  });

  it('should not call cancelBooking if confirmation is rejected', () => {
    spyOn(window, 'confirm').and.returnValue(false);
    component.cancelBooking(1);
    expect(bookingServiceMock.cancelBooking).not.toHaveBeenCalled();
  });

  it('should show appropriate message if there are no bookings', () => {
    bookingServiceMock.getBookingsByRenterId.and.returnValue(of([]));
    component.loadBookings();
    expect(component.bookings.length).toBe(0);
    expect(component.errorMessage).toBe('');
    expect(component.isLoading).toBeFalse();
  });

  it('should retrieve renterId from local storage if not present in user data', () => {
    spyOn(localStorage, 'getItem').and.returnValue(JSON.stringify({ renterId: 2 }));
    userServiceMock.userObservable = of({ user: null });
    component.ngOnInit();
    expect(component.renterId).toBe(2);
  });
});
