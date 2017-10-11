import { Component } from '@angular/core';
import { Room, ConfigService } from './config.service';
import { NavigationService } from './navigation.service';
import { HubService } from './hub.service';

@Component({
    selector: 'hc-menu',
    templateUrl: 'menu.component.html'
})
export class MenuComponent {
    Rooms: Room[];
    IsOpen: boolean;

    constructor(private navigationService: NavigationService, private configService: ConfigService) {
        this.Rooms = configService.GetRooms();
        this.IsOpen = false;
        configService.configChanged.subscribe((_: any) => { this.Rooms = configService.GetRooms(); })
    }

    toggleOpen() {
        this.IsOpen = !this.IsOpen;
    }

    navigateTo(room: Room) {
        this.navigationService.navigateToRoom(room);
        this.toggleOpen();
    }
}