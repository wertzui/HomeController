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
var ConfigService_1 = require('../../Services/ConfigService');
var RoomComponent = (function () {
    function RoomComponent(router, routeParams, configService) {
        var _this = this;
        this.router = router;
        this.routeParams = routeParams;
        this.configService = configService;
        this.Room = { Name: 'Getting Room from central config', Lights: [], Temperatures: [] };
        var name = this.routeParams.get('name');
        this.Room = this.configService.GetRoom(name);
        configService.configChanged.subscribe(function (o) { _this.Room = _this.configService.GetRoom(name); });
    }
    RoomComponent.prototype.ngOnInit = function () {
        console.log('init');
    };
    RoomComponent.prototype.ChannelChanged = function (channel) {
        this.configService.ChannelChanged(channel);
    };
    RoomComponent = __decorate([
        core_1.Component({
            selector: 'hc-room',
            templateUrl: 'app/components/room/room.html'
        }), 
        __metadata('design:paramtypes', [router_1.Router, router_1.RouteParams, ConfigService_1.ConfigService])
    ], RoomComponent);
    return RoomComponent;
})();
exports.RoomComponent = RoomComponent;
//# sourceMappingURL=room.js.map