import { Injectable } from "@angular/core";
import { HttpClient, HttpResponse, HttpHeaders } from "@angular/common/http";
import { Observable } from 'rxjs/Observable';
import { Swatch } from "./swatch.model";

@Injectable()
export class HttpService {
    
    private api = 'http://localhost:60716/api';
    
    constructor(private http : HttpClient) {}

    postPicture(formData : FormData) {

        //this.http.get(`${this.api}/pictures`).map(x => x.toString());
        return this.http.post<Swatch[]>(`${this.api}/pictures`, formData);
    }
}