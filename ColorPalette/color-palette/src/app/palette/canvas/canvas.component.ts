import { Component } from '@angular/core';
import { PaletteState } from 'src/app/store/reducers/palette.reducer';
import { Store } from '@ngrx/store';
import { getHasImageUrlSelector, getImageUrlSelector } from 'src/app/store/reducers';

@Component({
  selector: 'cp-canvas',
  templateUrl: './canvas.component.html',
  styleUrls: ['./canvas.component.less']
})
export class CanvasComponent {
  imageUrl$ = this.store.select(getImageUrlSelector);
  hasImageUrl$ = this.store.select(getHasImageUrlSelector);

  constructor(private store: Store<PaletteState>) { }
}
