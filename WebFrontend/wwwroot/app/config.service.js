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
const core_1 = require('@angular/core');
const hub_service_1 = require('./hub.service');
let ConfigService = class ConfigService {
    constructor(hub) {
        this.hub = hub;
        this.rooms = [];
        this.configChanged = new core_1.EventEmitter();
        this.onReceive = (message) => {
            if (message.Target == "WebFrontend") {
                //this.rooms = message.Values;
                this.UpdateChangedValues(this.rooms, message.Values);
                this.configChanged.emit(null);
            }
        };
        //this.rooms = [{ Name: "Wohnzimmer", Lights: [{ Name: "Deckenlicht", Channels: [{ Name: "W", Type: "Dimmer", Target: "ArtNet", Min: 0, Max: 255, Value: 100 }] }] }];
        this.hub.onReceive(this.onReceive);
    }
    UpdateChangedValues(oldObj, newObj) {
        var oldPropertyNames = Object.getOwnPropertyNames(oldObj);
        var newPropertyNames = Object.getOwnPropertyNames(newObj);
        // remove properties that are not present in newObj
        for (var oldPropNameIndex in oldPropertyNames) {
            var oldPropName = oldPropertyNames[oldPropNameIndex];
            if (!newObj[oldPropName]) {
                delete oldObj[oldPropName];
            }
        }
        // update and add all remaining properties
        for (var newPropNameIndex in newPropertyNames) {
            var newPropName = newPropertyNames[newPropNameIndex];
            var oldValue = oldObj[newPropName];
            var newValue = newObj[newPropName];
            if (oldValue !== newValue &&
                (oldValue === undefined || !oldValue.IsCurrentlyBeingModified)) {
                // value has changed => update
                var oldType = typeof oldValue;
                var newType = typeof newValue;
                if (oldType === newType && oldType === 'object') {
                    // we have an object or an array => go one level deeper
                    this.UpdateChangedValues(oldObj[newPropName], newObj[newPropName]);
                }
                else {
                    // types do not match or properties are not array or object => replace value
                    oldObj[newPropName] = newObj[newPropName];
                }
            }
        }
    }
    GetRooms() {
        return this.rooms;
    }
    GetRoom(name) {
        for (var room of this.rooms) {
            if (room.Name == name)
                return room;
        }
        return { Name: 'Unknown room', Lights: [], Temperatures: [] };
    }
    ChannelChanged(channel) {
        this.hub.sendMessage(channel.Target, [channel], hub_service_1.MessageMethod.Update);
        this.hub.sendMessage("FixtureRegister", [channel], hub_service_1.MessageMethod.Update);
        //console.log(JSON.stringify(channel));
    }
};
__decorate([
    core_1.Output(), 
    __metadata('design:type', Object)
], ConfigService.prototype, "configChanged", void 0);
ConfigService = __decorate([
    core_1.Injectable(), 
    __metadata('design:paramtypes', [hub_service_1.HubService])
], ConfigService);
exports.ConfigService = ConfigService;
//# sourceMappingURL=config.service.js.map