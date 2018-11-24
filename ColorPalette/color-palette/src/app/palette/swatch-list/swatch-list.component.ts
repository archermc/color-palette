import { Component, OnInit } from '@angular/core';
import { ColorPaletteService } from './../../shared/services/color-palette.service';

@Component({
  selector: 'cp-swatch-list',
  templateUrl: './swatch-list.component.html',
  styleUrls: ['./swatch-list.component.less']
})
export class SwatchListComponent implements OnInit {
  constructor(public colorPaletteService: ColorPaletteService) { }

  ngOnInit() { }
}
