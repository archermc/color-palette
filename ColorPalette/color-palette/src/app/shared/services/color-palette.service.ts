import { Injectable } from "@angular/core";
import { HttpService } from "./http.service";
import { Swatch } from "../models/swatch.model";
import { BehaviorSubject, Subject } from "rxjs";

@Injectable()
export class ColorPaletteService {
    fileContents: File;
    swatches: Subject<Swatch[]> = new Subject<Swatch[]>();
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

    uploadFile(): void {
        var formData = new FormData();
        formData.append('file', this.fileContents);

        this.httpService.postPicture(formData).subscribe(
            s => {
                this.swatches.next(s);
            },
            e => {
                console.log(e);
            });
    }
}