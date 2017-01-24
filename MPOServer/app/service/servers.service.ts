import {Injectable} from 'angular2/core';
import {Server} from '../models/server.model';
import {Http, Response, Headers} from 'angular2/http';
import 'rxjs/Rx';
import {Observable}     from 'rxjs/Observable';

@Injectable()
export class ServerService {

    public values: any;

    constructor(public _http: Http) { }

    private _serverUrl = 'http://localhost/api/servers/'

    getServers(env) {
        var url = this._serverUrl

        if (env)
            url += env;

        return this._http.get(url)
            .map(res => <Server[]>res.json())
            .catch(this.handleError);

    }

    handleError(error:Response) {
        console.error(error);
        return Observable.throw(error.json().error || 'Server error');
    }
}