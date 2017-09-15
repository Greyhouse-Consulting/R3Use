import { Assignment } from './../../models/assignment';
import { Configuration } from './../../app.constants';
import { Injectable } from '@angular/core';
import { Http, Response, Headers } from '@angular/http';
import 'rxjs/add/operator/map';
import { Observable } from 'rxjs/Observable';

@Injectable()
export class AssignmentService {

    private actionUrl: string;
    private headers: Headers;

    constructor(private http: Http, private configuration: Configuration) {

        this.actionUrl = configuration.Server + 'api/assignments/';

        this.headers = new Headers();
        this.headers.append('Content-Type', 'application/json');
        this.headers.append('Accept', 'application/json');
    }

    public GetAll = (): Observable<Assignment[]> => {
        return this.http.get(this.actionUrl, { headers: this.headers }).map((response: Response) => <Assignment[]>response.json());
    }

    public GetSingle = (id: number): Observable<Assignment> => {
        return this.http.get(this.actionUrl + id).map(res => <Assignment>res.json());
    }

    public Add = (assignment: Assignment): Observable<Assignment> => {
        let toAdd = JSON.stringify({ name: assignment.name });

        return this.http.post(this.actionUrl, toAdd, { headers: this.headers }).map(res => <Assignment>res.json());
    }

    public Update = (id: number, itemToUpdate: any): Observable<any> => {
        return this.http
            .put(this.actionUrl + id, JSON.stringify(itemToUpdate), { headers: this.headers });
    }

    public Delete = (id: number): Observable<any> => {
        return this.http.delete(this.actionUrl + id);
    }
}
