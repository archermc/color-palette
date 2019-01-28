import {
  ActionReducerMap,
  createFeatureSelector,
  createSelector,
  MetaReducer
} from '@ngrx/store';
import { environment } from '../../../environments/environment';
import * as fromPalette from './palette.reducer';

export interface State {

  palette: fromPalette.PaletteState;
}

export const reducers: ActionReducerMap<State> = {
  palette: fromPalette.reducer,
};


export const metaReducers: MetaReducer<State>[] = !environment.production ? [] : [];

export const selectPaletteState = createFeatureSelector<fromPalette.PaletteState>('palette');
export const getFileSelector = createSelector(selectPaletteState, fromPalette.getFile);
export const getImageUrlSelector = createSelector(selectPaletteState, fromPalette.getImageUrl);
export const getHasImageUrlSelector = createSelector(selectPaletteState, fromPalette.getHasImageUrl);
export const getSwatchesSelector = createSelector(selectPaletteState, fromPalette.getSwatches);
export const getHasSwatchesSelector = createSelector(selectPaletteState, fromPalette.getHasSwatches);