var __decorate = (this && this.__decorate) || function (decorators, target, key, desc) {
    var c = arguments.length, r = c < 3 ? target : desc === null ? desc = Object.getOwnPropertyDescriptor(target, key) : desc, d;
    if (typeof Reflect === "object" && typeof Reflect.decorate === "function") r = Reflect.decorate(decorators, target, key, desc);
    else for (var i = decorators.length - 1; i >= 0; i--) if (d = decorators[i]) r = (c < 3 ? d(r) : c > 3 ? d(target, key, r) : d(target, key)) || r;
    return c > 3 && r && Object.defineProperty(target, key, r), r;
};
var __metadata = (this && this.__metadata) || function (k, v) {
    if (typeof Reflect === "object" && typeof Reflect.metadata === "function") return Reflect.metadata(k, v);
};
import { Component } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { ConfigService } from './config.service';
let RoomComponent = class RoomComponent {
    constructor(route, configService) {
        this.route = route;
        this.configService = configService;
        this.Room = { Name: 'Getting Room from central config', Lights: [], Temperatures: [] };
        // var name = this.route.params['name'];
        // this.Room = this.configService.GetRoom(name);
        // configService.configChanged.subscribe(o => { this.Room = this.configService.GetRoom(name); })
    }
    ngOnInit() {
        this.route.params.forEach((params) => {
            const name = params['name'];
            this.Room = this.configService.GetRoom(name);
            this.configService.configChanged.subscribe((_) => { this.Room = this.configService.GetRoom(name); });
        });
    }
    ChannelChanged(channel) {
        this.configService.ChannelChanged(channel);
    }
    DisableUpdatesFor(channel) {
        channel.IsCurrentlyBeingModified = true;
    }
    EnableUpdatesFor(channel) {
        channel.IsCurrentlyBeingModified = false;
    }
    SwitchOn(channel) {
        channel.Value = Math.floor(channel.Max / 10);
        this.ChannelChanged(channel);
    }
    SwitchOff(channel) {
        channel.Value = channel.Min;
        this.ChannelChanged(channel);
    }
    ToggleChannel(channel) {
        if (channel.Value === channel.Min) {
            this.SwitchOn(channel);
        }
        else {
            this.SwitchOff(channel);
        }
    }
};
RoomComponent = __decorate([
    Component({
        selector: 'hc-room',
        templateUrl: 'room.component.html'
    }),
    __metadata("design:paramtypes", [ActivatedRoute,
        ConfigService])
], RoomComponent);
export { RoomComponent };
//# sourceMappingURL=room.component.js.map