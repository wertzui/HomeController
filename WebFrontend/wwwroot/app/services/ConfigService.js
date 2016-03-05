var __decorate = (this && this.__decorate) || function (decorators, target, key, desc) {
    var c = arguments.length, r = c < 3 ? target : desc === null ? desc = Object.getOwnPropertyDescriptor(target, key) : desc, d;
    if (typeof Reflect === "object" && typeof Reflect.decorate === "function") r = Reflect.decorate(decorators, target, key, desc);
    else for (var i = decorators.length - 1; i >= 0; i--) if (d = decorators[i]) r = (c < 3 ? d(r) : c > 3 ? d(target, key, r) : d(target, key)) || r;
    return c > 3 && r && Object.defineProperty(target, key, r), r;
};
var __metadata = (this && this.__metadata) || function (k, v) {
    if (typeof Reflect === "object" && typeof Reflect.metadata === "function") return Reflect.metadata(k, v);
};
var core_1 = require('angular2/core');
var HubService_1 = require('./HubService');
var ConfigService = (function () {
    function ConfigService(hub) {
        var _this = this;
        this.hub = hub;
        this.rooms = [];
        this.configChanged = new core_1.EventEmitter();
        this.onReceive = function (message) {
            if (message.Target == "WebFrontend") {
                _this.rooms = message.Values;
                _this.configChanged.emit(null);
            }
        };
        //this.rooms = [{ Name: "Wohnzimmer", Lights: [{ Name: "Deckenlicht", Channels: [{ Name: "W", Type: "Dimmer", Target: "ArtNet", Min: 0, Max: 255, Value: 100 }] }] }];
        this.hub.onReceive(this.onReceive);
    }
    ConfigService.prototype.GetRooms = function () {
        return this.rooms;
    };
    ConfigService.prototype.GetRoom = function (name) {
        for (var _i = 0, _a = this.rooms; _i < _a.length; _i++) {
            var room = _a[_i];
            if (room.Name == name)
                return room;
        }
        return { Name: 'Unknown room', Lights: [] };
    };
    ConfigService.prototype.ChannelChanged = function (channel) {
        this.hub.sendMessage(channel.Target, [channel], HubService_1.MessageMethod.Update);
        this.hub.sendMessage("FixtureRegister", [channel], HubService_1.MessageMethod.Update);
        //console.log(JSON.stringify(channel));
    };
    __decorate([
        core_1.Output(), 
        __metadata('design:type', Object)
    ], ConfigService.prototype, "configChanged", void 0);
    ConfigService = __decorate([
        core_1.Injectable(), 
        __metadata('design:paramtypes', [HubService_1.HubService])
    ], ConfigService);
    return ConfigService;
})();
exports.ConfigService = ConfigService;
//# sourceMappingURL=ConfigService.js.map