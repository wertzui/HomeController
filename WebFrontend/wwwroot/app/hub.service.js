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
const core_1 = require('@angular/core');
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
class Message {
}
exports.Message = Message;
let HubService_1 = class HubService {
    constructor() {
        // start the connection
        HubService_1.connection = $.hubConnection("http://Server:1906/signalr");
        HubService_1.eventHubProxy = HubService_1.connection.createHubProxy("EventHub");
        var hub = this;
        HubService_1.connection.start()
            .done(function () {
            console.log('Now connected, connection ID=' + HubService_1.connection.id);
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
    sendMessage(target, values, methodType) {
        var message = this.createMessage(target, values, methodType);
        return this.invokeOnServer(ServerMethod.Send, message);
    }
    onReceive(callBack) {
        return HubService_1.eventHubProxy.on("Receive", callBack);
    }
    createMessage(target, values, methodType) {
        return {
            Sender: "WebFrontend",
            Target: target,
            Time: new Date(),
            Values: values,
            Method: MessageMethod[methodType]
        };
    }
    invokeOnServer(method, message) {
        return HubService_1.eventHubProxy.invoke(ServerMethod[method], message)
            .fail(function (ex) {
            console.log("Error sending " + JSON.stringify(message));
            console.log(JSON.stringify(ex));
        })
            .done(function () {
            console.log("Sent " + JSON.stringify(message));
        });
    }
    joinGroup(groupName) {
        return this.invokeOnServer(ServerMethod.JoinGroup, groupName);
    }
    ;
    leaveGroup(groupName) {
        return this.invokeOnServer(ServerMethod.JoinGroup, groupName);
    }
    ;
};
let HubService = HubService_1;
HubService = HubService_1 = __decorate([
    core_1.Injectable(), 
    __metadata('design:paramtypes', [])
], HubService);
exports.HubService = HubService;
//# sourceMappingURL=hub.service.js.map