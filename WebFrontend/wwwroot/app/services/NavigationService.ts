import {Injectable} from 'angular2/core';
import {Router} from 'angular2/router';
import {Room} from './ConfigService'

@Injectable()
export class NavigationService {
    router: Router;

    constructor(router: Router) {
        this.router = router;
    }

    navigateToRoom(room: Room) {
        this.router.navigate(['Room', { name: room.Name }]);
    }
}