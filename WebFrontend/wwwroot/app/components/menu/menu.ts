import {Component} from 'angular2/core';
import {Room, ConfigService} from '../../Services/ConfigService';
import {NavigationService} from '../../Services/NavigationService';
import {HubService} from '../../Services/HubService';

@Component({
    selector: 'hc-menu',
    templateUrl: 'app/components/menu/menu.html'
})
export class MenuComponent {
    Rooms: Room[];
    IsOpen: boolean;

    constructor(private navigationService: NavigationService, private configService: ConfigService) {
        this.Rooms = configService.GetRooms();
        this.IsOpen = false;
        configService.configChanged.subscribe(o => { this.Rooms = configService.GetRooms(); })
    }

    toggleOpen() {
        this.IsOpen = !this.IsOpen;
    }

    navigateTo(room: Room) {
        this.navigationService.navigateToRoom(room);
    }
}