"use strict";
var __decorate = (this && this.__decorate) || function (decorators, target, key, desc) {
    var c = arguments.length, r = c < 3 ? target : desc === null ? desc = Object.getOwnPropertyDescriptor(target, key) : desc, d;
    if (typeof Reflect === "object" && typeof Reflect.decorate === "function") r = Reflect.decorate(decorators, target, key, desc);
    else for (var i = decorators.length - 1; i >= 0; i--) if (d = decorators[i]) r = (c < 3 ? d(r) : c > 3 ? d(target, key, r) : d(target, key)) || r;
    return c > 3 && r && Object.defineProperty(target, key, r), r;
};
var __metadata = (this && this.__metadata) || function (k, v) {
    if (typeof Reflect === "object" && typeof Reflect.metadata === "function") return Reflect.metadata(k, v);
};
var core_1 = require('@angular/core');
var router_1 = require('@angular/router');
var config_service_1 = require('./config.service');
var RoomComponent = (function () {
    function RoomComponent(route, configService) {
        this.route = route;
        this.configService = configService;
        this.Room = { Name: 'Getting Room from central config', Lights: [], Temperatures: [] };
        //var name = this.route.params['name'];
        //this.Room = this.configService.GetRoom(name);
        //configService.configChanged.subscribe(o => { this.Room = this.configService.GetRoom(name); })
    }
    RoomComponent.prototype.ngOnInit = function () {
        var _this = this;
        this.route.params.forEach(function (params) {
            var name = params['name'];
            _this.Room = _this.configService.GetRoom(name);
            _this.configService.configChanged.subscribe(function (o) { _this.Room = _this.configService.GetRoom(name); });
        });
    };
    RoomComponent.prototype.ChannelChanged = function (channel) {
        this.configService.ChannelChanged(channel);
    };
    RoomComponent.prototype.DisableUpdatesFor = function (channel) {
        channel.IsCurrentlyBeingModified = true;
    };
    RoomComponent.prototype.EnableUpdatesFor = function (channel) {
        channel.IsCurrentlyBeingModified = false;
    };
    RoomComponent.prototype.SwitchOn = function (channel) {
        channel.Value = channel.Max;
        this.ChannelChanged(channel);
    };
    RoomComponent.prototype.SwitchOff = function (channel) {
        channel.Value = channel.Min;
        this.ChannelChanged(channel);
    };
    RoomComponent.prototype.ToggleChannel = function (channel) {
        if (channel.Value == channel.Min) {
            this.SwitchOn(channel);
        }
        else {
            this.SwitchOff(channel);
        }
    };
    RoomComponent = __decorate([
        core_1.Component({
            selector: 'hc-room',
            templateUrl: 'app/room.component.html'
        }), 
        __metadata('design:paramtypes', [router_1.ActivatedRoute, config_service_1.ConfigService])
    ], RoomComponent);
    return RoomComponent;
}());
exports.RoomComponent = RoomComponent;
//# sourceMappingURL=room.component.js.map