import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Params } from '@angular/router';
import { Room, Channel, ConfigService } from './config.service';

@Component({
    selector: 'hc-room',
    templateUrl: 'app/room.component.html'
})
export class RoomComponent implements OnInit {
    Room: Room = { Name: 'Getting Room from central config', Lights: [], Temperatures: [] };

    constructor(
        private route: ActivatedRoute,
        private configService: ConfigService) {
        //var name = this.route.params['name'];
        //this.Room = this.configService.GetRoom(name);
        //configService.configChanged.subscribe(o => { this.Room = this.configService.GetRoom(name); })
    }

    ngOnInit() {
        this.route.params.forEach((params: Params) => {
            let name = params['name'];
            this.Room = this.configService.GetRoom(name);
            this.configService.configChanged.subscribe(o => { this.Room = this.configService.GetRoom(name); });
        });
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
        channel.Value = Math.floor(channel.Max / 10);
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