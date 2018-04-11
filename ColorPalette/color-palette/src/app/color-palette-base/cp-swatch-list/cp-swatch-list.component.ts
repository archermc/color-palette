import { Component, OnInit } from '@angular/core';
import { Swatch } from '../shared/swatch.model';
import { ColorPaletteService } from '../shared/color-palette.service';
import { Observable } from 'rxjs/observable';

@Component({
  selector: 'cp-swatch-list',
  templateUrl: './cp-swatch-list.component.html',
  styleUrls: ['./cp-swatch-list.component.css']
})
export class CpSwatchListComponent implements OnInit {

  swatches : Swatch[];

  constructor(private colorPaletteService : ColorPaletteService) { }

  ngOnInit() {
    this.colorPaletteService.swatchesObservable.subscribe(res => {
      console.log('UPDOOTING');
      console.log(res);
      this.swatches = res;
    });
  }

}
