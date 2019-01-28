import { Injectable } from '@angular/core';
import { Actions, Effect, ofType } from '@ngrx/effects';
import { Observable } from 'rxjs';
import { switchMap, withLatestFrom, map, catchError } from 'rxjs/operators'
import { Action, Store } from '@ngrx/store';
import * as paletteActions from '../actions/palette.actions';
import { ColorPaletteService } from './../../shared/services/color-palette.service';
import { PaletteState } from './../reducers/palette.reducer';
import { Swatch } from 'src/app/shared/models/swatch.model';
import { getFileSelector } from '../reducers';

@Injectable()
export class PaletteEffects {
  getFile$ = this.store.select(getFileSelector);
  
  constructor(private actions$: Actions,
    private colorPaletteService: ColorPaletteService,
    private store: Store<PaletteState>
  ) {}

  @Effect()
  uploadFile$: Observable<Action> = this.actions$.pipe(
    ofType(paletteActions.PaletteActionTypes.UPLOAD_FILE),
    withLatestFrom(this.getFile$),
    switchMap(([, file]) => {
      return this.colorPaletteService.uploadFile(file)
        .pipe(
          map((swatches: Swatch[]) => {
            return new paletteActions.UploadFileSucceeded(swatches);
          }),
        )
    })
  );
}
