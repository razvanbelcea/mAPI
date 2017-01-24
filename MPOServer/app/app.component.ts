import {Component} from 'angular2/core';
import {RouteConfig, RouterOutlet, RouterLink} from 'angular2/router'

import {MainServerPage} from './component/mainserverpage.component'
import {BarcodeComponent} from './component/barcodes.component'
import {NotFoundComponent} from './component/notfound.component'



@RouteConfig([
    { path: '/servers', name: 'MainServer', component: MainServerPage, useAsDefault: true },
    { path: '/barcodes', name: 'Barcode', component: BarcodeComponent },
    {path: '/*other', name: 'Others', component: NotFoundComponent }
])
@Component({
        selector: 'my-app',
        styles: [`
.left {
    float: left;
}
.right {
    float: left;
    margin: 20px;
}  
`],
        templateUrl: 'template/navbar.template.html',
    directives: [RouterOutlet, RouterLink]
})
export class AppComponent { }