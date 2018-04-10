import { Component, OnInit } from '@angular/core';
import { ColorPaletteService } from '../shared/color-palette.service';

@Component({
  selector: 'cp-file',
  templateUrl: './cp-file.component.html',
  styleUrls: ['./cp-file.component.css']
})
export class CpFileComponent implements OnInit {

  constructor(private colorPaletteService : ColorPaletteService) { }

  ngOnInit() {
  }

  onChange(contents : File) {
    console.log(contents);
    this.colorPaletteService.changeFileContents(contents);
  }
}
