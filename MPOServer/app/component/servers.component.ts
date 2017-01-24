import {Component, OnInit, Input, ViewChild, ElementRef} from 'angular2/core';
import {Router, RouterLink} from 'angular2/router';
import {Http, Response, HTTP_PROVIDERS} from 'angular2/http';

import {Server} from '../models/server.model'
import {ServerService} from '../service/servers.service'
import {SearchPipe} from '../component/search.pipe'


@Component({
    selector: 'servers',
    pipes: [SearchPipe],
    templateUrl: 'template/servers.template.html',
    directives: [RouterLink],
    providers: [ServerService, HTTP_PROVIDERS]
})
export class ServersComponent implements OnInit {
    @ViewChild('input')
    input: ElementRef;
    data: any[];


    constructor(private _serverService: ServerService, private _router: Router) { }
    
        errorMessage: string;
        public servers: Server[];
        isLoading = true;

        selectedServer: Server;

        ngOnInit() {
          //  this.getServers();
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
