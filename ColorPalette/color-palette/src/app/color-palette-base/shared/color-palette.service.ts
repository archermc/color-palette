import { Injectable } from "@angular/core";
import { HttpService } from "./http.service";
import { Swatch } from "./swatch.model";
import {Observable} from 'rxjs';
import 'rxjs/add/observable/of';

@Injectable()
export class ColorPaletteService {
    fileContents : File;
    shit : Swatch[] = new Array(new Swatch (3, 3, 3));
    swatchesObservable : Observable<Swatch[]> = Observable.of(this.shit);

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

    uploadFile() : void {
        var formData = new FormData();
        formData.append('file', this.fileContents);

        // THIS PART IS THE CURRENT ISSUE.  It should update the swatch-list's component with the new swatch array value, but it isn't.  Cool.
        this.httpService.postPicture(formData).subscribe(s => {
            this.shit = s;
        });

        return;
        //this.swatches = s;
    }

    // getSwatches(): Observable<Swatch[]> {
    //     return this.swatches;
    // }
}