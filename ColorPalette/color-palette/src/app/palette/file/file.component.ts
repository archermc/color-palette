import { Component, OnInit } from '@angular/core';
import { ColorPaletteService } from 'src/app/shared/services/color-palette.service';

@Component({
  selector: 'cp-file',
  templateUrl: './file.component.html',
  styleUrls: ['./file.component.less']
})
export class FileComponent implements OnInit {

  constructor(private colorPaletteService: ColorPaletteService) { }

  ngOnInit() {
  }

  onChange(contents: File) {
    console.log(contents);
    this.colorPaletteService.changeFileContents(contents);
  }
}
