import { ComponentFixture, TestBed } from '@angular/core/testing';

import { RegisterAsDealerComponent } from './register-as-dealer.component';

describe('RegisterAsDealerComponent', () => {
  let component: RegisterAsDealerComponent;
  let fixture: ComponentFixture<RegisterAsDealerComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [RegisterAsDealerComponent]
    })
    .compileComponents();

    fixture = TestBed.createComponent(RegisterAsDealerComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
