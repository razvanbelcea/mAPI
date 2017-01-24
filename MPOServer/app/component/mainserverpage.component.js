System.register(['angular2/core', 'angular2/router', 'angular2/http', '../component/servers.component', '../component/till.component', '../component/search.pipe', '../service/servers.service'], function(exports_1, context_1) {
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
    var core_1, router_1, http_1, servers_component_1, till_component_1, search_pipe_1, servers_service_1;
    var MainServerPage;
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
            function (servers_component_1_1) {
                servers_component_1 = servers_component_1_1;
            },
            function (till_component_1_1) {
                till_component_1 = till_component_1_1;
            },
            function (search_pipe_1_1) {
                search_pipe_1 = search_pipe_1_1;
            },
            function (servers_service_1_1) {
                servers_service_1 = servers_service_1_1;
            }],
        execute: function() {
            MainServerPage = (function () {
                function MainServerPage(_serverService, _router) {
                    this._serverService = _serverService;
                    this._router = _router;
                    this.isLoading = true;
                }
                MainServerPage.prototype.ngOnInit = function () {
                    this.getServers('qa');
                };
                MainServerPage.prototype.reloadServers = function (env) {
                    this.servers = null;
                    this.getServers(env);
                };
                MainServerPage.prototype.getServers = function (env) {
                    var _this = this;
                    this._serverService.getServers(env)
                        .subscribe(function (value) {
                        _this.servers = value;
                        _this.isLoading = false;
                    }, function (error) { return _this.errorMessage = error; });
                };
                MainServerPage = __decorate([
                    core_1.Component({
                        selector: 'mainserverpage',
                        styles: ["\n.left {\n    float: left;\n}\n.right {\n    float: left;\n    margin: 20px;\n}"],
                        templateUrl: 'template/mainserverpage.template.html',
                        //template: `<div class="container-fluid">
                        //<servers class="left"></servers>
                        //<tills class="right"> </tills>
                        //</div>`,
                        directives: [till_component_1.TillComponent, servers_component_1.ServersComponent, router_1.RouterLink],
                        providers: [servers_service_1.ServerService, http_1.HTTP_PROVIDERS],
                        pipes: [search_pipe_1.SearchPipe]
                    }), 
                    __metadata('design:paramtypes', [servers_service_1.ServerService, router_1.Router])
                ], MainServerPage);
                return MainServerPage;
            }());
            exports_1("MainServerPage", MainServerPage);
        }
    }
});
//# sourceMappingURL=mainserverpage.component.js.map