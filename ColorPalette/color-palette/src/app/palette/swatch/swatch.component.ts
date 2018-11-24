import { Component, OnInit, Input } from '@angular/core';
import { Swatch } from 'src/app/shared/models/swatch.model';

@Component({
  selector: 'cp-swatch',
  template: `<div class='color-swatch' style.background-color='{{getBackgroundColor()}}'></div>`,
  styleUrls: ['./swatch.component.less']
})
export class SwatchComponent implements OnInit {
  @Input() swatch: Swatch;

  constructor() { }

  ngOnInit() {
  }

  getBackgroundColor() {
    var b = 'rgb(' + this.swatch.R + ',' + this.swatch.G + ',' + this.swatch.B + ')';
    return b;
  }
}
