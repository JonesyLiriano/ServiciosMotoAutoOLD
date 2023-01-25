import { HttpClient, HttpHeaders, HttpParams } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable, throwError } from 'rxjs';
import { catchError } from 'rxjs/operators';
import { environment } from 'src/environments/environment';


@Injectable({
    providedIn: 'root'
})
export class ApiService {
    options = { headers: new HttpHeaders().set('Content-Type', 'application/json'),
    observe: 'response' as 'body'};
    baseUrl = environment.serverApiUrl;
    constructor(private httpClient: HttpClient) { }

    public get(path: string, params: HttpParams = new HttpParams()): Observable<any> {
        return this.httpClient.get(this.baseUrl + path, { params }).pipe(catchError(this.formatErrors));
    }

    public put(path: string, body: object = {}): Observable<any> {
        return this.httpClient
            .put(this.baseUrl + path, JSON.stringify(body), this.options )
            .pipe(catchError(this.formatErrors));
    }

    public post(path: string, body: any = {}): Observable<any> {
        return this.httpClient
            .post(this.baseUrl + path, JSON.stringify(body), this.options)
            .pipe(catchError(this.formatErrors));
    }

    public delete(path: string): Observable<any> {
        return this.httpClient.delete(this.baseUrl + path).pipe(catchError(this.formatErrors));
    }

    public formatErrors(error: any): Observable<any> {
        console.log(JSON.stringify(error));
        return throwError(error.error);
    }
}
