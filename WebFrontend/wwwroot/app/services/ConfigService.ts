import {Injectable, Component, provide, OnInit, Output, EventEmitter} from 'angular2/core';
import {HubService, Message, MessageMethod} from './HubService';

export interface Channel {
    Name: string;
    Type: string;
    Target: string;
    Min: number;
    Max: number;
    Value: number;
    CanBeModified: boolean;
}

export interface Light {
    Name: string;
    Channels: Channel[];
}

export interface Temperature {
    Name: string;
    TargetTemperature: Channel;
    MeasuredTemperature: Channel;
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
        //this.rooms = [{ Name: "Wohnzimmer", Lights: [{ Name: "Deckenlicht", Channels: [{ Name: "W", Type: "Dimmer", Target: "ArtNet", Min: 0, Max: 255, Value: 100 }] }] }];
        this.hub.onReceive(this.onReceive);
    }

    private onReceive = (message: Message) => {
        if (message.Target == "WebFrontend") {
            this.rooms = message.Values;
            this.configChanged.emit(null);
        }
    }

    GetRooms() {
        return this.rooms;
    }

    GetRoom(name: string) {
        for (var room of this.rooms) {
            if (room.Name == name)
                return room;
        }
        return { Name: 'Unknown room', Lights: [], Temperatures: [] };
    }

    ChannelChanged(channel: Channel) {
        this.hub.sendMessage(channel.Target, [channel], MessageMethod.Update);
        this.hub.sendMessage("FixtureRegister", [channel], MessageMethod.Update);
        //console.log(JSON.stringify(channel));
    }
}
