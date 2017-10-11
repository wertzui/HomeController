var __decorate = (this && this.__decorate) || function (decorators, target, key, desc) {
    var c = arguments.length, r = c < 3 ? target : desc === null ? desc = Object.getOwnPropertyDescriptor(target, key) : desc, d;
    if (typeof Reflect === "object" && typeof Reflect.decorate === "function") r = Reflect.decorate(decorators, target, key, desc);
    else for (var i = decorators.length - 1; i >= 0; i--) if (d = decorators[i]) r = (c < 3 ? d(r) : c > 3 ? d(target, key, r) : d(target, key)) || r;
    return c > 3 && r && Object.defineProperty(target, key, r), r;
};
var __metadata = (this && this.__metadata) || function (k, v) {
    if (typeof Reflect === "object" && typeof Reflect.metadata === "function") return Reflect.metadata(k, v);
};
import { Injectable, Output, EventEmitter } from '@angular/core';
import { HubService, MessageMethod } from './hub.service';
let ConfigService = class ConfigService {
    constructor(hub) {
        this.hub = hub;
        this.rooms = [];
        this.configChanged = new EventEmitter();
        this.onReceive = (message) => {
            if (message.Target === 'WebFrontend') {
                this.UpdateChangedValues(this.rooms, message.Values);
                this.configChanged.emit(null);
            }
        };
        this.hub.onReceive(this.onReceive);
    }
    UpdateChangedValues(oldObj, newObj) {
        const oldPropertyNames = Object.getOwnPropertyNames(oldObj);
        const newPropertyNames = Object.getOwnPropertyNames(newObj);
        // remove properties that are not present in newObj
        for (const oldPropName of oldPropertyNames) {
            // const oldPropName = oldPropertyNames[oldPropNameIndex];
            if (!newObj[oldPropName]) {
                delete oldObj[oldPropName];
            }
        }
        // update and add all remaining properties
        for (const newPropName of newPropertyNames) {
            // const newPropName = newPropertyNames[newPropNameIndex];
            const oldValue = oldObj[newPropName];
            const newValue = newObj[newPropName];
            if (oldValue !== newValue &&
                (oldValue === undefined || !oldValue.IsCurrentlyBeingModified)) {
                // value has changed => update
                const oldType = typeof oldValue;
                const newType = typeof newValue;
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
        for (const room of this.rooms) {
            if (room.Name === name)
                return room;
        }
        return { Name: 'Unknown room', Lights: [], Temperatures: [] };
    }
    ChannelChanged(channel) {
        this.hub.sendMessage(channel.Target, [channel], MessageMethod.Update);
        this.hub.sendMessage('FixtureRegister', [channel], MessageMethod.Update);
    }
};
__decorate([
    Output(),
    __metadata("design:type", Object)
], ConfigService.prototype, "configChanged", void 0);
ConfigService = __decorate([
    Injectable(),
    __metadata("design:paramtypes", [HubService])
], ConfigService);
export { ConfigService };
//# sourceMappingURL=config.service.js.map