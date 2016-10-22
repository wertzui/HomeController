import {Component, OnInit} from 'angular2/core';
import {Router, RouteParams} from 'angular2/router';
import {Room, Channel, ConfigService} from '../../Services/ConfigService';
import {FormatPipe} from '../../pipes/format.pipe';

@Component({
    selector: 'hc-room',
    templateUrl: 'app/components/room/room.html',
    pipes: [FormatPipe]
})
export class RoomComponent implements OnInit {
    Room: Room = { Name: 'Getting Room from central config', Lights: [], Temperatures: [] };

    constructor(
        private router: Router,
        private routeParams: RouteParams,
        private configService: ConfigService) {
        var name = this.routeParams.get('name');
        this.Room = this.configService.GetRoom(name);
        configService.configChanged.subscribe(o => { this.Room = this.configService.GetRoom(name); })
    }

    ngOnInit() {
        console.log('init');
    }

    ChannelChanged(channel: Channel) {
        this.configService.ChannelChanged(channel);
    }

    DisableUpdatesFor(channel: Channel) {
        channel.IsCurrentlyBeingModified = true;
    }

    EnableUpdatesFor(channel: Channel) {
        channel.IsCurrentlyBeingModified = false;
    }

    SwitchOn(channel: Channel) {
        channel.Value = channel.Max;
        this.ChannelChanged(channel);
    }

    SwitchOff(channel: Channel) {
        channel.Value = channel.Min;
        this.ChannelChanged(channel);
    }

    ToggleChannel(channel: Channel) {
        if (channel.Value == channel.Min) {
            this.SwitchOn(channel);
        }
        else {
            this.SwitchOff(channel);
        }
    }
}