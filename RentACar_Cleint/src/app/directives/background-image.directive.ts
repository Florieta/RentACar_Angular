import { Directive, Renderer2, ElementRef, Input } from '@angular/core';

@Directive({
  selector: '[appBackgroundImage]',
  standalone: true,
})
export class BackgroundImageDirective {
  @Input('appBackgroundImage') backgroundImageUrl: string = '';

  constructor(private renderer: Renderer2, private el: ElementRef) {}

  ngOnInit() {
    if (this.backgroundImageUrl) {
      this.renderer.setStyle(
        this.el.nativeElement,
        'backgroundImage',
        `url(${this.backgroundImageUrl})`
      );
      this.renderer.setStyle(this.el.nativeElement, 'backgroundSize', 'cover');
      this.renderer.setStyle(this.el.nativeElement, 'backgroundPosition', 'center');
      this.renderer.setStyle(this.el.nativeElement, 'filter', 'brightness(50%)');
    }
  }
}
