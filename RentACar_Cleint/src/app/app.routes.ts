import { Routes } from '@angular/router';
import { HomeComponent } from './home/home.component';
import { CatalogComponent } from './catalog/catalog.component';
import { LoginComponent } from './login/login.component';
import { RegisterAsDealerComponent } from './register-as-dealer/register-as-dealer.component';
import { RegisterAsRenterComponent } from './register-as-renter/register-as-renter.component';
import { MyProfileComponent } from './my-profile/my-profile.component';

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
    { path: 'my-profile', component: MyProfileComponent },
];
