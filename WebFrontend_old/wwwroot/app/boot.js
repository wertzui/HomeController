"use strict";
///<reference path="../../node_modules/angular2/typings/browser.d.ts"/>
///<reference path="../../typings/main.d.ts"/>
var browser_1 = require('angular2/platform/browser');
var core_1 = require('angular2/core');
var router_1 = require('angular2/router');
var HubService_1 = require('./services/HubService');
var ConfigService_1 = require('./services/ConfigService');
var app_1 = require('./app');
var NavigationService_1 = require('./services/NavigationService');
var bootstrap_application = function () {
    var providers = [
        router_1.ROUTER_PROVIDERS,
        NavigationService_1.NavigationService,
        HubService_1.HubService,
        ConfigService_1.ConfigService,
        core_1.provide(router_1.LocationStrategy, { useClass: router_1.HashLocationStrategy })];
    browser_1.bootstrap(app_1.App, providers);
};
bootstrap_application();
//# sourceMappingURL=boot.js.map