import { Injectable, Component, OnInit, Output, EventEmitter } from '@angular/core';
import { HubService, Message, MessageMethod } from './hub.service';

export interface Channel {
    Name: string;
    Type: string;
    Target: string;
    Min: number;
    Max: number;
    Value: number;
    CanBeModified: boolean;
    IsCurrentlyBeingModified: boolean;
}

export interface Light {
    Name: string;
    Channels: Channel[];
}

export interface Temperature {
    Name: string;
    TargetTemperature: Channel;
    MeasuredTemperature: Channel;
    MeasuredHumidity: Channel;
}

export interface Room {
    Name: string;
    Lights: Light[];
    Temperatures: Temperature[];
}

@Injectable()
export class ConfigService {
    private rooms: Room[] = [];
    @Output() configChanged = new EventEmitter();

    constructor(private hub: HubService) {
        this.hub.onReceive(this.onReceive);
    }

    private onReceive = (message: Message) => {
        if (message.Target === 'WebFrontend') {
            this.UpdateChangedValues(this.rooms, message.Values);
            this.configChanged.emit(null);
        }
    }

    private UpdateChangedValues(oldObj: any, newObj: any) {
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

    GetRoom(name: string) {
        for (const room of this.rooms) {
            if (room.Name === name)
                return room;
        }
        return { Name: 'Unknown room', Lights: [], Temperatures: [] };
    }

    ChannelChanged(channel: Channel) {
        this.hub.sendMessage(channel.Target, [channel], MessageMethod.Update);
        this.hub.sendMessage('FixtureRegister', [channel], MessageMethod.Update);
    }
}
