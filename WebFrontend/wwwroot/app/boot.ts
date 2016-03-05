///<reference path="../../node_modules/angular2/typings/browser.d.ts"/>
///<reference path="../../typings/main.d.ts"/>
import {bootstrap} from 'angular2/platform/browser'
import {provide} from 'angular2/core'
import {ROUTER_PROVIDERS, LocationStrategy, HashLocationStrategy} from 'angular2/router'
import {HubService} from './services/HubService';
import {ConfigService} from './services/ConfigService';
import {App} from './app';
import {NavigationService} from './services/NavigationService';

var bootstrap_application = function () {
    var providers = [
        ROUTER_PROVIDERS,
        NavigationService,
        HubService,
        ConfigService,
        provide(LocationStrategy, { useClass: HashLocationStrategy })];
    bootstrap(App, providers);
};

bootstrap_application();