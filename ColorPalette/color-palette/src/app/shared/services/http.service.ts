import { Injectable } from "@angular/core";
import { HttpClient } from "@angular/common/http";
import { Swatch } from "../models/swatch.model";
import { Observable } from "rxjs";

@Injectable()
export class HttpService {
    // TODO: get from environment file
    private api = 'http://localhost:49700/api';

    constructor(private http: HttpClient) { }

    postPicture(formData: FormData): Observable<Swatch[]> {

        //this.http.get(`${this.api}/pictures`).map(x => x.toString());
        return this.http.post<Swatch[]>(`${this.api}/pictures`, formData);
    }
}