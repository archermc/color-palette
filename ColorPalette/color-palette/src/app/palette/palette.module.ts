import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { LandingPageComponent } from './landing-page/landing-page.component';
import { ButtonComponent } from './button/button.component';
import { CanvasComponent } from './canvas/canvas.component';
import { SwatchListComponent } from './swatch-list/swatch-list.component';
import { SwatchComponent } from './swatch/swatch.component';
import { FileComponent } from './file/file.component';
import { ColorPaletteService } from '../shared/services/color-palette.service';

@NgModule({
  declarations: [
    LandingPageComponent,
    ButtonComponent,
    CanvasComponent,
    SwatchComponent,
    SwatchListComponent,
    FileComponent
  ],
  imports: [
    CommonModule
  ],
  providers: [
    ColorPaletteService
  ]
})
export class PaletteModule { }
