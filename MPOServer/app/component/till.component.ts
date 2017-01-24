import {Component, OnInit} from 'angular2/core';
import {Router} from 'angular2/router';
import {Http, Response, HTTP_PROVIDERS} from 'angular2/http';

import {Till} from '../models/till.model';
import {TillService} from '../service/till.service';

@Component({
    selector: 'tills',
    templateUrl: 'template/tills.template.html',
    providers: [TillService, HTTP_PROVIDERS]

})

export class TillComponent implements OnInit {

    constructor(private _tillservice: TillService, private _router: Router) { }

    errorMessage: string;
    public tills: Till[];

    selectedTill: Till;

    ngOnInit() {
        this.getTills();
    }

    onSelect(till: Till) {
        this.selectedTill = till;
    }

    getTills() {
        this._tillservice.getTills()
            .subscribe(
            value => this.tills = value,
            error => this.errorMessage = <any>error);           
    }

    gotoDetail() {
        this._router.navigate(['TillDetails', { id: this.selectedTill.szWorkstationID }]);
    }
}