import { Injectable } from "@angular/core";
import { HttpClient } from "@angular/common/http";
import { Swatch } from "../models/swatch.model";
import { Observable } from "rxjs";
import { API_URL } from "../../../environments/environment";

@Injectable()
export class HttpService {
    constructor(private http: HttpClient) { }

    postPicture(formData: FormData): Observable<Swatch[]> {
        return this.http.post<Swatch[]>(`${API_URL}/pictures`, formData);
    }
}