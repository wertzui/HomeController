import {Component, OnInit} from 'angular2/core';
import {Router, RouteParams} from 'angular2/router';
import {Room, Channel, ConfigService} from '../../Services/ConfigService';

@Component({
    selector: 'hc-room',
    templateUrl: 'app/components/room/room.html'
})
export class RoomComponent implements OnInit{
    Room: Room = { Name: 'Getting Room from central config', Lights: [] };

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
}