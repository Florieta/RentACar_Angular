import { ComponentFixture, TestBed } from '@angular/core/testing';

import { RegisterAsRenterComponent } from './register-as-renter.component';

describe('RegisterAsRenterComponent', () => {
  let component: RegisterAsRenterComponent;
  let fixture: ComponentFixture<RegisterAsRenterComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [RegisterAsRenterComponent]
    })
    .compileComponents();

    fixture = TestBed.createComponent(RegisterAsRenterComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
