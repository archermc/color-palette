import { Component, OnInit } from '@angular/core';
import { PaletteState } from 'src/app/store/reducers/palette.reducer';
import { Store } from '@ngrx/store';
import { StoreFile } from 'src/app/store/actions/palette.actions';

@Component({
  selector: 'cp-file',
  templateUrl: './file.component.html',
  styleUrls: ['./file.component.less']
})
export class FileComponent {

  constructor(private store: Store<PaletteState>) { }

  onChange(contents: File) {
    var fileReader = new FileReader();

    fileReader.onload = () => {
      this.store.dispatch(new StoreFile({file: contents, imageUrl: fileReader.result as string}));
    }

    fileReader.readAsDataURL(contents);
  }
}
