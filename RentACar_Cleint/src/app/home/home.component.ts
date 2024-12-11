import { Component } from '@angular/core';
import { Router } from '@angular/router';
import { BackgroundImageDirective } from '../directives/background-image.directive';
import { UserService } from '../user/user.service';
import { trigger, transition, style, animate } from '@angular/animations';
import { CommonModule } from '@angular/common';

@Component({
  selector: 'app-home',
  standalone: true,
  imports: [BackgroundImageDirective, CommonModule],
  templateUrl: './home.component.html',
  styleUrl: './home.component.css',
  animations: [
    trigger('slideAnimation', [
      transition(':increment', [
        style({ transform: 'translateX(100%)' }),  
        animate('500ms ease-in', style({ transform: 'translateX(0%)' }))  
      ]),
      transition(':decrement', [
        style({ transform: 'translateX(-100%)' }), 
        animate('500ms ease-in', style({ transform: 'translateX(0%)' })) 
      ])
    ])
  ]
})

export class HomeComponent {
  isLogged: boolean = false;

  images: string[] = [
    '/assets/cars/2.png',
    '/assets/cars/Citroen-C4-3.jpg',
    '/assets/cars/1.webp',
  ];

  currentSlide: number = 0;
  slideInterval: any;

  constructor(private router: Router, private userService: UserService) {
    this.userService.userObservable.subscribe((user) => {
      if (user) {
        this.isLogged = true;
      } else {
        this.isLogged = false;
      }
    });
  }

  onRegister() {
    this.router.navigate(['/register/renter']);
  }

  prevSlide() {
    if (this.currentSlide === 0) {
      this.currentSlide = this.images.length - 1;
    } else {
      this.currentSlide--;
    }
  }

  nextSlide() {
    if (this.currentSlide === this.images.length - 1) {
      this.currentSlide = 0;
    } else {
      this.currentSlide++;
    }
  }
}
