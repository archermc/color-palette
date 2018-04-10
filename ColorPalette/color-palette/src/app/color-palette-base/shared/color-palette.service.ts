import { Injectable } from "@angular/core";
import { HttpService } from "./http.service";

@Injectable()
export class ColorPaletteService {
    fileContents : File;

    constructor(private httpService : HttpService) {}

    changeFileContents(contents) {
        this.fileContents = contents;
    }

    hasFileToUpload() : boolean {
        if (this.fileContents) {
            return true;
        }

        return false;
    }

    uploadFile() {
        var formData = new FormData();
        formData.append('file', this.fileContents);
        this.httpService.postPicture(formData);
    }
}