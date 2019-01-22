import { Injectable } from "@angular/core";
import { HttpService } from "./http.service";
import { Swatch } from "../models/swatch.model";
import { BehaviorSubject, Subject, Observable } from "rxjs";

@Injectable()
export class ColorPaletteService {
    fileContents: File;
    imgUrl: any;

    constructor(private httpService: HttpService) { }

    changeFileContents(contents) {
        this.fileContents = contents;

        var fileReader = new FileReader();

        fileReader.onload = () => {
            this.imgUrl = fileReader.result;
        }

        fileReader.readAsDataURL(contents);
    }

    hasFileToUpload(): boolean {
        return !!this.fileContents;
    }

    uploadFile(): Observable<Swatch[]> {
        var formData = new FormData();
        formData.append('file', this.fileContents);

        return this.httpService.postPicture(formData);
    }
}