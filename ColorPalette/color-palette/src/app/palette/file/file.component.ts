import { Component, OnInit } from '@angular/core';
import { PaletteState } from 'src/app/store/reducers/palette.reducer';
import { Store } from '@ngrx/store';
import { StoreFile } from 'src/app/store/actions/palette.actions';
import { Observable } from 'rxjs';
import { getFileSelector } from 'src/app/store/reducers';

@Component({
  selector: 'cp-file',
  template: `
    <button mat-raised-button primary (click)="file.click()">Choose Files</button>
    <input hidden type="file" (change)="onChange(file.files[0])" #file>
    <span>{{(file$ | async)?.name}}</span>
  `,
  styles: [`button { margin-right: 10px; }`]
})
export class FileComponent implements OnInit {
  file$: Observable<File>;

  constructor(private store: Store<PaletteState>) { }

  ngOnInit() {
    this.file$ = this.store.select(getFileSelector);
  }

  onChange(contents: File) {
    var fileReader = new FileReader();

    fileReader.onload = () => {
      this.store.dispatch(new StoreFile({file: contents, imageUrl: fileReader.result as string}));
    }

    fileReader.readAsDataURL(contents);
  }
}
