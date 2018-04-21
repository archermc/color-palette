import { Component, OnInit } from '@angular/core';
import {ColorPaletteService} from '../shared/color-palette.service';

@Component({
  selector: 'cp-canvas',
  templateUrl: './cp-canvas.component.html',
  styleUrls: ['./cp-canvas.component.css']
})
export class CpCanvasComponent implements OnInit {

  constructor(private colorPaletteService : ColorPaletteService) { }

  ngOnInit() {
  }

  getImageUrl() {
    return this.colorPaletteService.imgUrl;
  }
}
