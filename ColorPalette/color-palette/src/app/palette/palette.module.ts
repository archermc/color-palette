import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { LandingPageComponent } from './landing-page/landing-page.component';
import { CanvasComponent } from './canvas/canvas.component';
import { SwatchListComponent } from './swatch-list/swatch-list.component';
import { SwatchComponent } from './swatch/swatch.component';
import { FileComponent } from './file/file.component';
import { ColorPaletteService } from '../shared/services/color-palette.service';
import * as material from '@angular/material';

@NgModule({
  declarations: [
    LandingPageComponent,
    CanvasComponent,
    SwatchComponent,
    SwatchListComponent,
    FileComponent
  ],
  imports: [
    CommonModule,
    material.MatToolbarModule,
    material.MatButtonModule
  ],
  providers: [
    ColorPaletteService
  ]
})
export class PaletteModule { }
