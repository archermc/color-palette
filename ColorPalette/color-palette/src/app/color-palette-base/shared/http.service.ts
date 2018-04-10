import { Injectable } from "@angular/core";
import { HttpClient, HttpResponse, HttpHeaders } from "@angular/common/http";
import { Observable } from 'rxjs/Observable';

@Injectable()
export class HttpService {
    
    private api = 'http://localhost:60716/api';
    
    constructor(private http : HttpClient) {}

    postPicture(formData : FormData) {
        this.http.post(`${this.api}/pictures`, formData)
            .subscribe(res => {
                console.log(res);
                return res;
            }, err => {
                throw err;
            });
    }
}