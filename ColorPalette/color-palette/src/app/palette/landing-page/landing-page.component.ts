import { Component } from '@angular/core';
import { PaletteState } from 'src/app/store/reducers/palette.reducer';
import { Store } from '@ngrx/store';
import * as paletteActions from 'src/app/store/actions/palette.actions';
import * as paletteSelectors from './../../store/reducers/index';

@Component({
  selector: 'cp-landing-page',
  templateUrl: './landing-page.component.html',
  styleUrls: ['./landing-page.component.less']
})
export class LandingPageComponent {
  public hasImageUrl$ = this.store.select(paletteSelectors.getHasImageUrlSelector);
  public hasSwatches$ = this.store.select(paletteSelectors.getHasSwatchesSelector);
  public swatches$ = this.store.select(paletteSelectors.getSwatchesSelector);

  constructor(private store: Store<PaletteState>) { }

  uploadImage() {
    this.store.dispatch(new paletteActions.UploadFile());
  }

  resetImage() {
    this.store.dispatch(new paletteActions.ResetFile());
  }
}
