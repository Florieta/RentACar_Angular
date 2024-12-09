import { ComponentFixture, TestBed } from '@angular/core/testing';
import { AddCarComponent } from './add-car.component';
import { CarService } from '../car.service';
import { UserService } from '../../user/user.service';
import { Router } from '@angular/router';
import { FormsModule } from '@angular/forms';
import { of, throwError } from 'rxjs';
import { CommonModule } from '@angular/common';
import { LoaderComponent } from '../../shared/loader/loader.component';

describe('AddCarComponent', () => {
  let component: AddCarComponent;
  let fixture: ComponentFixture<AddCarComponent>;
  let carServiceMock: any;
  let userServiceMock: any;
  let routerMock: any;

  beforeEach(async () => {
    carServiceMock = {
      getCategories: jasmine.createSpy('getCategories').and.returnValue(of([{ id: 1, categoryName: 'SUV' }])),
      createCar: jasmine.createSpy('createCar').and.returnValue(of({})),
    };

    userServiceMock = {
      userObservable: of({ user: { dealerId: 42 } }),
    };

    routerMock = {
      navigate: jasmine.createSpy('navigate'),
    };

    await TestBed.configureTestingModule({
      declarations: [AddCarComponent],
      imports: [CommonModule, FormsModule, LoaderComponent],
      providers: [
        { provide: CarService, useValue: carServiceMock },
        { provide: UserService, useValue: userServiceMock },
        { provide: Router, useValue: routerMock },
      ],
    }).compileComponents();

    fixture = TestBed.createComponent(AddCarComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create the component', () => {
    expect(component).toBeTruthy();
  });

  it('should initialize with user and categories', () => {
    expect(component.dealerId).toBe(42);
    expect(carServiceMock.getCategories).toHaveBeenCalled();
    expect(component.categories).toEqual([{ id: 1, categoryName: 'SUV' }]);
  });

  it('should handle form submission successfully', () => {
    component.formValues = {
      regNumber: 'ABC123',
      model: 'ModelX',
      make: 'Tesla',
      makeYear: 2022,
      airCondition: true,
      seats: 4,
      doors: 4,
      navigationSystem: true,
      fuel: 'Electric',
      transmission: 'Automatic',
      dailyRate: 100,
      imageUrl: 'image.jpg',
      categoryId: 1,
      dealerId: 42,
    };

    const formMock = {
      invalid: false,
    } as any;

    component.submitCar(formMock);
    expect(carServiceMock.createCar).toHaveBeenCalledWith(component.formValues);
    expect(routerMock.navigate).toHaveBeenCalledWith(['/my-cars']);
  });

  it('should handle invalid form submission', () => {
    const formMock = {
      invalid: true,
    } as any;

    component.submitCar(formMock);
    expect(carServiceMock.createCar).not.toHaveBeenCalled();
  });

  it('should handle image upload successfully', async () => {
    const fileMock = new File(['image content'], 'image.jpg', { type: 'image/jpeg' });
    const eventMock = {
      target: { files: [fileMock] },
    } as unknown as Event;

    spyOn(window, 'fetch').and.returnValue(
      Promise.resolve({ json: () => Promise.resolve({}) } as Response)
    );

    component.onFileChange(eventMock);
    await fixture.whenStable();
    expect(component.formValues.imageUrl).toBe('image.jpg');
  });

  it('should handle image upload failure', async () => {
    const fileMock = new File(['image content'], 'image.jpg', { type: 'image/jpeg' });
    const eventMock = {
      target: { files: [fileMock] },
    } as unknown as Event;

    spyOn(window, 'fetch').and.returnValue(Promise.reject('Upload failed'));
    spyOn(console, 'error');

    component.onFileChange(eventMock);
    await fixture.whenStable();
    expect(console.error).toHaveBeenCalledWith('Error during image upload:', 'Upload failed');
  });

  it('should handle category fetch failure', () => {
    carServiceMock.getCategories.and.returnValue(throwError('Error fetching categories'));
    component.ngOnInit();
    expect(console.error).toHaveBeenCalledWith('Error fetching categories:', 'Error fetching categories');
  });
});
