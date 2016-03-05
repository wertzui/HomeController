var __decorate = (this && this.__decorate) || function (decorators, target, key, desc) {
    var c = arguments.length, r = c < 3 ? target : desc === null ? desc = Object.getOwnPropertyDescriptor(target, key) : desc, d;
    if (typeof Reflect === "object" && typeof Reflect.decorate === "function") r = Reflect.decorate(decorators, target, key, desc);
    else for (var i = decorators.length - 1; i >= 0; i--) if (d = decorators[i]) r = (c < 3 ? d(r) : c > 3 ? d(target, key, r) : d(target, key)) || r;
    return c > 3 && r && Object.defineProperty(target, key, r), r;
};
var __metadata = (this && this.__metadata) || function (k, v) {
    if (typeof Reflect === "object" && typeof Reflect.metadata === "function") return Reflect.metadata(k, v);
};
var core_1 = require('angular2/core');
var router_1 = require('angular2/router');
var menu_1 = require('./components/menu/menu');
var home_1 = require('./components/home/home');
var room_1 = require('./components/room/room');
var ConfigService_1 = require('./Services/ConfigService');
var NavigationService_1 = require('./Services/NavigationService');
var HubService_1 = require('./Services/HubService');
var App = (function () {
    function App() {
    }
    App = __decorate([
        core_1.Component({
            selector: 'hc',
            templateUrl: 'app/app.html',
            providers: [NavigationService_1.NavigationService, ConfigService_1.ConfigService, HubService_1.HubService],
            directives: [router_1.ROUTER_DIRECTIVES, menu_1.MenuComponent]
        }),
        router_1.RouteConfig([
            { path: "/", name: "Home", component: home_1.HomeComponent, useAsDefault: true },
            { path: "/room/:name", name: "Room", component: room_1.RoomComponent }
        ]), 
        __metadata('design:paramtypes', [])
    ], App);
    return App;
})();
exports.App = App;
//# sourceMappingURL=app.js.map