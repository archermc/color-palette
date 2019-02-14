import { Component, OnInit, Input } from '@angular/core';
import { Swatch } from 'src/app/shared/models/swatch.model';

@Component({
  selector: 'cp-swatch',
  template: `<div class="color-swatch" [style.background-color]=""></div>`,
  styleUrls: ['./swatch.component.less']
})
export class SwatchComponent {
  @Input() swatch: Swatch;

  constructor() { }

  public backgroundColor() {
    var b = 'rgb(' + Number(this.swatch.R) + ',' + Number(this.swatch.G) + ',' + (this.swatch.B) + ')';
    return b;
  }
}
