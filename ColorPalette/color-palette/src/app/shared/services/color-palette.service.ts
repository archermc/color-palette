import { Injectable } from "@angular/core";
import { HttpService } from "./http.service";
import { Swatch } from "../models/swatch.model";
import { Observable } from "rxjs";

@Injectable()
export class ColorPaletteService {
    constructor(private httpService: HttpService) { }

    uploadFile(file: File): Observable<Swatch[]> {
        var formData = new FormData();
        formData.append('file', file);

        return this.httpService.postPicture(formData);
    }
}