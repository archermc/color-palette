import { Component, OnInit } from '@angular/core';
import { ColorPaletteService } from 'src/app/shared/services/color-palette.service';

@Component({
  selector: 'cp-landing-page',
  templateUrl: './landing-page.component.html',
  styleUrls: ['./landing-page.component.less']
})
export class LandingPageComponent implements OnInit {
  constructor(
    private colorPaletteService: ColorPaletteService
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
