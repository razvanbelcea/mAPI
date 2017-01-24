System.register(['angular2/core', 'angular2/router', './component/mainserverpage.component', './component/barcodes.component', './component/notfound.component'], function(exports_1, context_1) {
    "use strict";
    var __moduleName = context_1 && context_1.id;
    var __decorate = (this && this.__decorate) || function (decorators, target, key, desc) {
        var c = arguments.length, r = c < 3 ? target : desc === null ? desc = Object.getOwnPropertyDescriptor(target, key) : desc, d;
        if (typeof Reflect === "object" && typeof Reflect.decorate === "function") r = Reflect.decorate(decorators, target, key, desc);
        else for (var i = decorators.length - 1; i >= 0; i--) if (d = decorators[i]) r = (c < 3 ? d(r) : c > 3 ? d(target, key, r) : d(target, key)) || r;
        return c > 3 && r && Object.defineProperty(target, key, r), r;
    };
    var __metadata = (this && this.__metadata) || function (k, v) {
        if (typeof Reflect === "object" && typeof Reflect.metadata === "function") return Reflect.metadata(k, v);
    };
    var core_1, router_1, mainserverpage_component_1, barcodes_component_1, notfound_component_1;
    var AppComponent;
    return {
        setters:[
            function (core_1_1) {
                core_1 = core_1_1;
            },
            function (router_1_1) {
                router_1 = router_1_1;
            },
            function (mainserverpage_component_1_1) {
                mainserverpage_component_1 = mainserverpage_component_1_1;
            },
            function (barcodes_component_1_1) {
                barcodes_component_1 = barcodes_component_1_1;
            },
            function (notfound_component_1_1) {
                notfound_component_1 = notfound_component_1_1;
            }],
        execute: function() {
            AppComponent = (function () {
                function AppComponent() {
                }
                AppComponent = __decorate([
                    router_1.RouteConfig([
                        { path: '/servers', name: 'MainServer', component: mainserverpage_component_1.MainServerPage, useAsDefault: true },
                        { path: '/barcodes', name: 'Barcode', component: barcodes_component_1.BarcodeComponent },
                        { path: '/*other', name: 'Others', component: notfound_component_1.NotFoundComponent }
                    ]),
                    core_1.Component({
                        selector: 'my-app',
                        styles: ["\n.left {\n    float: left;\n}\n.right {\n    float: left;\n    margin: 20px;\n}  \n"],
                        templateUrl: 'template/navbar.template.html',
                        directives: [router_1.RouterOutlet, router_1.RouterLink]
                    }), 
                    __metadata('design:paramtypes', [])
                ], AppComponent);
                return AppComponent;
            }());
            exports_1("AppComponent", AppComponent);
        }
    }
});
//# sourceMappingURL=app.component.js.map