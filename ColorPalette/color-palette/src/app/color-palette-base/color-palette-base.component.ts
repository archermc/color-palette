import { Component, OnInit } from '@angular/core';
import { Observable } from 'rxjs/Observable';
import { ColorPaletteService } from './shared/color-palette.service';
import { HttpService } from './shared/http.service';

@Component({
  selector: 'color-palette-base',
  templateUrl: './color-palette-base.component.html',
  styleUrls: ['./color-palette-base.component.css']
})
export class ColorPaletteBaseComponent implements OnInit {

  constructor(
    private colorPaletteService : ColorPaletteService
  ) { }

  ngOnInit() {

  }

  uploadImage() {
    if (this.colorPaletteService.hasFileToUpload()) {
      this.colorPaletteService.uploadFile();
    } else {
      // figure out how to make some html elements outlined in red
    }
  }

  resetImage() {
    console.log('image resetting');
  }
}
