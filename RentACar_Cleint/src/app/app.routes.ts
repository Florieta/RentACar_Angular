import { Routes } from '@angular/router';
import { HomeComponent } from './home/home.component';
import { CatalogComponent } from './catalog/catalog.component';
import { LoginComponent } from './user/login/login.component';
import { RegisterAsDealerComponent } from './user/register-as-dealer/register-as-dealer.component';
import { RegisterAsRenterComponent } from './user/register-as-renter/register-as-renter.component';
import { MyProfileComponent } from './user/my-profile/my-profile.component';
import { AuthGuard } from './guards/auth.guard';
import { CarDetailComponent } from './catalog/car-details/car-details.component';
import { MyCarsComponent } from './mycars/mycars.component';
import { MyBookingsComponent } from './mybookings/mybookings.component';
import { BookingComponent } from './create-booking/create-booking.component';
import { ErrorPageComponent } from './error-page/error-page.component';

export const routes: Routes = [
    { path: '', redirectTo: '/home', pathMatch: 'full' },
    { path: 'home', component: HomeComponent },
  
    { path: 'catalog', component: CatalogComponent },
    { path: 'login', component: LoginComponent },
    {
        path: 'register',
        children: [
        { path: 'dealer', component:  RegisterAsDealerComponent},
        { path: 'renter', component: RegisterAsRenterComponent },
        ],
    },
    { path: 'my-profile', component: MyProfileComponent, canActivate: [AuthGuard] },
    { path: 'car/:id', component: CarDetailComponent }, 
    { path: 'my-cars', component: MyCarsComponent }, 
    { path: 'my-bookings', component: MyBookingsComponent },
    { path: 'booking', component: BookingComponent },
    { path: '404', component: ErrorPageComponent },
    { path: '**', redirectTo: '/404' },
];
