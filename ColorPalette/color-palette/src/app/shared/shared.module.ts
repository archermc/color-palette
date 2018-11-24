import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { ColorPaletteService } from './services/color-palette.service';
import { HttpService } from './services/http.service';
import { HttpClientModule } from '@angular/common/http';

@NgModule({
  declarations: [],
  imports: [
    CommonModule,
    HttpClientModule
  ],
  providers: [
    ColorPaletteService,
    HttpService
  ]
})
export class SharedModule { }
