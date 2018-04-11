import { Component, OnInit, Input } from '@angular/core';
import { Swatch } from '../shared/swatch.model';

@Component({
  selector: 'cp-swatch',
  templateUrl: './cp-swatch.component.html',
  styleUrls: ['./cp-swatch.component.css']
})
export class CpSwatchComponent implements OnInit {
  @Input() swatch : Swatch;

  constructor() { }

  ngOnInit() {
  }

}
