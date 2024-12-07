import { Pipe, PipeTransform } from '@angular/core';

@Pipe({
  name: 'averageRating',
  standalone: true,
})
export class AverageRatingPipe implements PipeTransform {
  transform(ratings: number[]): number {
    if (!ratings || ratings.length === 0) {
      return 0; 
    }
    const total = ratings.reduce((sum, rating) => sum + rating, 0);

    const average = total / ratings.length;

    return Math.round(average); 
  }

}