import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';

import { AppComponent } from './app.component';
import { ColorPaletteBaseComponent } from './color-palette-base/color-palette-base.component';
import { CpButtonComponent } from './color-palette-base/cp-button/cp-button.component';
import { CpCanvasComponent } from './color-palette-base/cp-canvas/cp-canvas.component';
import { CpSwatchListComponent } from './color-palette-base/cp-swatch-list/cp-swatch-list.component';
import { CpSwatchComponent } from './color-palette-base/cp-swatch/cp-swatch.component';
import { ColorPaletteService } from './color-palette-base/shared/color-palette.service';
import { CpFileComponent } from './color-palette-base/cp-file/cp-file.component';
import { HttpService } from './color-palette-base/shared/http.service';
import { HttpClientModule } from '@angular/common/http';

@NgModule({
  declarations: [
    AppComponent,
    ColorPaletteBaseComponent,
    CpButtonComponent,
    CpCanvasComponent,
    CpSwatchListComponent,
    CpSwatchComponent,
    CpFileComponent
  ],
  imports: [
    BrowserModule,
    HttpClientModule
  ],
  providers: [
    ColorPaletteService,
    HttpService
  ],
  bootstrap: [AppComponent]
})
export class AppModule { }
