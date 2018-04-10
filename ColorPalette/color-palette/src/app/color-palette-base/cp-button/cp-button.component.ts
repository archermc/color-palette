import { Component, OnInit, Input, Output, EventEmitter } from '@angular/core';
import { Observable } from 'rxjs/Observable';

@Component({
  selector: 'cp-button',
  templateUrl: './cp-button.component.html',
  styleUrls: ['./cp-button.component.css'],
  host: {
    '(click)': 'onClick($event)'
  }
})
export class CpButtonComponent implements OnInit {
  @Input() label: string;
  @Output() buttonClicked: EventEmitter<any> = new EventEmitter();

  constructor() { }

  ngOnInit() {
  }

  onClick(event) {
    console.log(`clicked ${this.label}`);
    this.buttonClicked.emit(event);
  }
}