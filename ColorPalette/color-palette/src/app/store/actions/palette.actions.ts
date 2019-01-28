import { Action } from '@ngrx/store';
import { Swatch } from 'src/app/shared/models/swatch.model';

export enum PaletteActionTypes {
  UPLOAD_FILE = '[Palette] Upload File',
  UPLOAD_FILE_SUCCEEDED = '[Palette] Upload File Succeeded',
  UPLOAD_FILE_FAILED = '[Palette] Upload File Failed',
  STORE_FILE = '[Palette] Store File',
  RESET_FILE = '[Palette] Reset File'
}

export class UploadFile implements Action {
  readonly type = PaletteActionTypes.UPLOAD_FILE;
}

export class UploadFileSucceeded implements Action {
  readonly type = PaletteActionTypes.UPLOAD_FILE_SUCCEEDED;
  constructor(public payload: Swatch[]) {}
}

export class UploadFileFailed implements Action {
  readonly type = PaletteActionTypes.UPLOAD_FILE_FAILED;
  constructor(public payload: string) {}
}

export class StoreFile implements Action {
  readonly type = PaletteActionTypes.STORE_FILE;
  constructor(public payload: {file: File, imageUrl: string}) {}
}

export class ResetFile implements Action {
  readonly type = PaletteActionTypes.RESET_FILE;
}

export type PaletteActions = 
  UploadFile |
  UploadFileSucceeded |
  UploadFileFailed |
  StoreFile |
  ResetFile;
