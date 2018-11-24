import { Component, OnInit, Input, Output, EventEmitter } from '@angular/core';

@Component({
  selector: 'cp-button',
  templateUrl: './button.component.html',
  styleUrls: ['./button.component.less'],
  host: {
    '(click)': 'onClick($event)'
  }
})
export class ButtonComponent implements OnInit {
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