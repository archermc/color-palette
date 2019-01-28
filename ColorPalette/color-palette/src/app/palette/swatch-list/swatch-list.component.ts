import { Component, OnInit } from '@angular/core';
import { PaletteState } from 'src/app/store/reducers/palette.reducer';
import { Store } from '@ngrx/store';
import { getSwatchesSelector } from 'src/app/store/reducers';

@Component({
  selector: 'cp-swatch-list',
  templateUrl: './swatch-list.component.html',
  styleUrls: ['./swatch-list.component.less']
})
export class SwatchListComponent {
  swatches$ = this.store.select(getSwatchesSelector);
  
  constructor(private store: Store<PaletteState>) { }
}
