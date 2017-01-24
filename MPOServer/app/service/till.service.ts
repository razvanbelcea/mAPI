import {Injectable} from 'angular2/core';
import {Till} from '../models/till.model';
import {Http, Response, Headers} from 'angular2/http';
import 'rxjs/Rx';
import {Observable}     from 'rxjs/Observable';

@Injectable()
export class TillService {

    public values: any;

    constructor(public _http: Http) { }

    private _tillsUrl = 'http://localhost:57220/api/tills/asd'; // URL to web api

    getTills() {
        return this._http.get(this._tillsUrl)
            .map(res => <Till[]>res.json())
            .catch(this.handleError);
    }
    private handleError(error: Response) {
        console.error(error);
        return Observable.throw(error.json().error || 'Server error');
    }
}
