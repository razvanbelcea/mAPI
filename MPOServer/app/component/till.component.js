System.register(['angular2/core', 'angular2/router', 'angular2/http', '../service/till.service'], function(exports_1, context_1) {
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
    var core_1, router_1, http_1, till_service_1;
    var TillComponent;
    return {
        setters:[
            function (core_1_1) {
                core_1 = core_1_1;
            },
            function (router_1_1) {
                router_1 = router_1_1;
            },
            function (http_1_1) {
                http_1 = http_1_1;
            },
            function (till_service_1_1) {
                till_service_1 = till_service_1_1;
            }],
        execute: function() {
            TillComponent = (function () {
                function TillComponent(_tillservice, _router) {
                    this._tillservice = _tillservice;
                    this._router = _router;
                }
                TillComponent.prototype.ngOnInit = function () {
                    this.getTills();
                };
                TillComponent.prototype.onSelect = function (till) {
                    this.selectedTill = till;
                };
                TillComponent.prototype.getTills = function () {
                    var _this = this;
                    this._tillservice.getTills()
                        .subscribe(function (value) { return _this.tills = value; }, function (error) { return _this.errorMessage = error; });
                };
                TillComponent.prototype.gotoDetail = function () {
                    this._router.navigate(['TillDetails', { id: this.selectedTill.szWorkstationID }]);
                };
                TillComponent = __decorate([
                    core_1.Component({
                        selector: 'tills',
                        templateUrl: 'template/tills.template.html',
                        providers: [till_service_1.TillService, http_1.HTTP_PROVIDERS]
                    }), 
                    __metadata('design:paramtypes', [till_service_1.TillService, router_1.Router])
                ], TillComponent);
                return TillComponent;
            }());
            exports_1("TillComponent", TillComponent);
        }
    }
});
//# sourceMappingURL=till.component.js.map