import { Injectable } from '@angular/core';
import { Router } from '@angular/router';
import { Room } from './config.service'

@Injectable()
export class NavigationService {
    router: Router;

    constructor(router: Router) {
        this.router = router;
    }

    navigateToRoom(room: Room) {
        let link = ['/room', room.Name];
        this.router.navigate(link);
    }
}