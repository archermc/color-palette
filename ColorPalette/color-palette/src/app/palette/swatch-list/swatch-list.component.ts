import { Component, Input } from '@angular/core';
import { Swatch } from 'src/app/shared/models/swatch.model';

@Component({
  selector: 'cp-swatch-list',
  templateUrl: './swatch-list.component.html',
  styleUrls: ['./swatch-list.component.less']
})
export class SwatchListComponent {  
  @Input()
  public swatches: Swatch[];
}
