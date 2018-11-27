import { Component, OnInit } from '@angular/core';
import { ColorPaletteService } from './../../shared/services/color-palette.service';
import { Swatch } from 'src/app/shared/models/swatch.model';

@Component({
  selector: 'cp-swatch-list',
  templateUrl: './swatch-list.component.html',
  styleUrls: ['./swatch-list.component.less']
})
export class SwatchListComponent implements OnInit {
  swatches: Swatch[];

  constructor(public colorPaletteService: ColorPaletteService) { }

  ngOnInit() {
    this.colorPaletteService.swatches.subscribe(
      s => { this.swatches = s; }
    );
  }
}
