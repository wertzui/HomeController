"use strict";
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
//export abstract class IHubService {
//    abstract sendMessage(target: string, values: any, methodType: MessageMethod);
//    abstract onReceive(callBack: (message: any) => any);
//}
(function (MessageMethod) {
    MessageMethod[MessageMethod["Get"] = 0] = "Get";
    MessageMethod[MessageMethod["Update"] = 1] = "Update";
})(exports.MessageMethod || (exports.MessageMethod = {}));
var MessageMethod = exports.MessageMethod;
var ServerMethod;
(function (ServerMethod) {
    ServerMethod[ServerMethod["Send"] = 0] = "Send";
    ServerMethod[ServerMethod["JoinGroup"] = 1] = "JoinGroup";
    ServerMethod[ServerMethod["LeaveGroup"] = 2] = "LeaveGroup";
})(ServerMethod || (ServerMethod = {}));
var Message = (function () {
    function Message() {
    }
    return Message;
}());
exports.Message = Message;
var HubService = (function () {
    function HubService() {
        // start the connection
        HubService.connection = $.hubConnection("http://Server:1906/signalr");
        HubService.eventHubProxy = HubService.connection.createHubProxy("EventHub");
        var hub = this;
        HubService.connection.start()
            .done(function () {
            console.log('Now connected, connection ID=' + HubService.connection.id);
            hub.joinGroup("WebFrontend").done(function () {
                hub.sendMessage("FixtureRegister", null, MessageMethod.Get);
            });
        })
            .fail(function () {
            console.log('Could not connect');
        });
        // log incoming messages
        // as incoming messages are async, we cannot do it the same way as send, but have to register to the onReceive event.
        this.onReceive(function (message) {
            console.log("Receive " + JSON.stringify(message));
        });
        //super();
    }
    HubService.prototype.sendMessage = function (target, values, methodType) {
        var message = this.createMessage(target, values, methodType);
        return this.invokeOnServer(ServerMethod.Send, message);
    };
    HubService.prototype.onReceive = function (callBack) {
        return HubService.eventHubProxy.on("Receive", callBack);
    };
    HubService.prototype.createMessage = function (target, values, methodType) {
        return {
            Sender: "WebFrontend",
            Target: target,
            Time: new Date(),
            Values: values,
            Method: MessageMethod[methodType]
        };
    };
    HubService.prototype.invokeOnServer = function (method, message) {
        return HubService.eventHubProxy.invoke(ServerMethod[method], message)
            .fail(function (ex) {
            console.log("Error sending " + JSON.stringify(message));
            console.log(JSON.stringify(ex));
        })
            .done(function () {
            console.log("Sent " + JSON.stringify(message));
        });
    };
    HubService.prototype.joinGroup = function (groupName) {
        return this.invokeOnServer(ServerMethod.JoinGroup, groupName);
    };
    ;
    HubService.prototype.leaveGroup = function (groupName) {
        return this.invokeOnServer(ServerMethod.JoinGroup, groupName);
    };
    ;
    HubService = __decorate([
        core_1.Injectable(), 
        __metadata('design:paramtypes', [])
    ], HubService);
    return HubService;
}());
exports.HubService = HubService;
//# sourceMappingURL=HubService.js.map