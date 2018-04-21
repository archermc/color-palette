import { Injectable } from "@angular/core";
import { HttpService } from "./http.service";
import { Swatch } from "./swatch.model";
import {Observable} from 'rxjs';
import 'rxjs/add/observable/of';

@Injectable()
export class ColorPaletteService {
    fileContents : File;
    swatches : Swatch[];
    imgUrl : any;

    constructor(private httpService : HttpService) {}

    changeFileContents(contents) {
        this.fileContents = contents;

        var fileReader = new FileReader();

        fileReader.onload = () => {
            this.imgUrl = fileReader.result;
        }

        fileReader.readAsDataURL(contents);
    }

    hasFileToUpload() : boolean {
        if (this.fileContents) {
            return true;
        }

        return false;
    }

    uploadFile() : void {
        var formData = new FormData();
        formData.append('file', this.fileContents);

        this.httpService.postPicture(formData).subscribe(s => {
            console.log(s);
            this.swatches = s;
        });
    }
}