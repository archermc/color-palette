import { Component, OnInit } from '@angular/core';
import { ColorPaletteService } from 'src/app/shared/services/color-palette.service';

@Component({
  selector: 'cp-canvas',
  templateUrl: './canvas.component.html',
  styleUrls: ['./canvas.component.less']
})
export class CanvasComponent implements OnInit {

  constructor(private colorPaletteService: ColorPaletteService) { }

  ngOnInit() {
  }

  getImageUrl() {
    return this.colorPaletteService.imgUrl;
  }
}
