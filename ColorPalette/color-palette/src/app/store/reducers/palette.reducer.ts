import * as paletteActions from '../actions/palette.actions';
import { Swatch } from 'src/app/shared/models/swatch.model';


export interface PaletteState {
  file: any;
  imageUrl: string;
  swatches: Swatch[];
}

export const initialState: PaletteState = {
  file: undefined,
  imageUrl: undefined,
  swatches: []
};

export function reducer(state = initialState, action: paletteActions.PaletteActions): PaletteState {
  switch (action.type) {
    case paletteActions.PaletteActionTypes.STORE_FILE:
      return {
        ...state,
        file: action.payload.file,
        imageUrl: action.payload.imageUrl
      };

    case paletteActions.PaletteActionTypes.UPLOAD_FILE_SUCCEEDED:
      return {
        ...state,
        swatches: action.payload
      }

    case paletteActions.PaletteActionTypes.RESET_FILE:
      return initialState;

    default:
      return state;
  }
}

export const getFile = (state: PaletteState) => state.file;
export const getImageUrl = (state: PaletteState) => state.imageUrl;
export const getHasImageUrl = (state: PaletteState) => state.imageUrl !== undefined;
export const getSwatches = (state: PaletteState) => state.swatches;
export const getHasSwatches = (state: PaletteState) => state.swatches.length !== 0;