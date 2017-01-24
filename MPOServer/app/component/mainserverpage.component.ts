import {Component} from 'angular2/core';
import {Router, RouterLink} from 'angular2/router';
import {Http, Response, HTTP_PROVIDERS} from 'angular2/http';

import {ServersComponent} from '../component/servers.component'
import {TillComponent} from '../component/till.component'
import {SearchPipe} from '../component/search.pipe'
import {Server} from '../models/server.model'
import {ServerService} from '../service/servers.service'

@Component({
    selector: 'mainserverpage',
    styles: [`
.left {
    float: left;
}
.right {
    float: left;
    margin: 20px;
}`],
    templateUrl: 'template/mainserverpage.template.html',
    //template: `<div class="container-fluid">
    //<servers class="left"></servers>
    //<tills class="right"> </tills>
    //</div>`,
    directives: [TillComponent, ServersComponent, RouterLink],
    providers: [ServerService, HTTP_PROVIDERS],
    pipes: [SearchPipe]
})
export class MainServerPage {

    constructor(private _serverService: ServerService, private _router: Router) { }

    errorMessage: string;
    public servers: Server[];
    isLoading = true;
    selectedServer: Server;

    ngOnInit() {
          this.getServers('qa');
    }

    reloadServers(env) {
        this.servers = null;

        this.getServers(env);
    }

    getServers(env?) {
        this._serverService.getServers(env)
            .subscribe(
            value => {
                this.servers = value;
                this.isLoading = false;
            },
            error => this.errorMessage = <any>error);
    }

}