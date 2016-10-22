import {Component} from 'angular2/core';
import {ROUTER_DIRECTIVES, RouteConfig} from 'angular2/router'
import {MenuComponent} from './components/menu/menu';
import {HomeComponent} from './components/home/home';
import {RoomComponent} from './components/room/room';
import {ConfigService} from './Services/ConfigService';
import {NavigationService} from './Services/NavigationService';
import {HubService} from './Services/HubService';

@Component({
    selector: 'hc',
    templateUrl: 'app/app.html',
    providers: [NavigationService, ConfigService, HubService],
    directives: [ROUTER_DIRECTIVES, MenuComponent]
})
@RouteConfig([
    { path: "/", name: "Home", component: HomeComponent, useAsDefault: true },
    { path: "/room/:name", name: "Room", component: RoomComponent }
])
export class App {
}