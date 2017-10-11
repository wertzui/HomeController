var __decorate = (this && this.__decorate) || function (decorators, target, key, desc) {
    var c = arguments.length, r = c < 3 ? target : desc === null ? desc = Object.getOwnPropertyDescriptor(target, key) : desc, d;
    if (typeof Reflect === "object" && typeof Reflect.decorate === "function") r = Reflect.decorate(decorators, target, key, desc);
    else for (var i = decorators.length - 1; i >= 0; i--) if (d = decorators[i]) r = (c < 3 ? d(r) : c > 3 ? d(target, key, r) : d(target, key)) || r;
    return c > 3 && r && Object.defineProperty(target, key, r), r;
};
var __metadata = (this && this.__metadata) || function (k, v) {
    if (typeof Reflect === "object" && typeof Reflect.metadata === "function") return Reflect.metadata(k, v);
};
import { Injectable } from '@angular/core';
import { HubConnection } from '@aspnet/signalr-client';
// export abstract class IHubService {
//     abstract sendMessage(target: string, values: any, methodType: MessageMethod);
//     abstract onReceive(callBack: (message: any) => any);
// }
export var MessageMethod;
(function (MessageMethod) {
    MessageMethod[MessageMethod["Get"] = 0] = "Get";
    MessageMethod[MessageMethod["Update"] = 1] = "Update";
})(MessageMethod || (MessageMethod = {}));
var ServerMethod;
(function (ServerMethod) {
    ServerMethod[ServerMethod["Send"] = 0] = "Send";
    ServerMethod[ServerMethod["JoinGroup"] = 1] = "JoinGroup";
    ServerMethod[ServerMethod["LeaveGroup"] = 2] = "LeaveGroup";
})(ServerMethod || (ServerMethod = {}));
export class Message {
}
let HubService = HubService_1 = class HubService {
    constructor() {
        // start the connection
        HubService_1.connection = new HubConnection('http://localhost:1906/');
        //HubService.eventHubProxy = HubService.connection.createHubProxy('EventHub');
        HubService_1.connection.onclose(() => {
            console.log('SignalR disconnected, trying to reconnect.');
            setTimeout(() => {
                this.startConnection();
            }, HubService_1.reconnectDelay);
        });
        // log incoming messages
        // as incoming messages are async, we cannot do it the same way as send, but have to register to the onReceive event.
        this.onReceive(function (message) {
            console.log('Receive ' + JSON.stringify(message));
        });
        this.startConnection();
    }
    startConnection() {
        const hub = this;
        HubService_1.connection.start()
            .then(function () {
            console.log('Now connected');
            hub.joinGroup('WebFrontend')
                .then(_ => hub.sendMessage('FixtureRegister', null, MessageMethod.Get));
        })
            .catch(function () {
            console.log('Could not connect');
        });
    }
    sendMessage(target, values, methodType) {
        const message = this.createMessage(target, values, methodType);
        return this.invokeOnServer(ServerMethod.Send, message);
    }
    onReceive(callBack) {
        return HubService_1.connection.on('Receive', callBack);
    }
    createMessage(target, values, methodType) {
        return {
            Sender: 'WebFrontend',
            Target: target,
            Time: new Date(),
            Values: values,
            Method: MessageMethod[methodType]
        };
    }
    invokeOnServer(method, message) {
        return HubService_1.connection.invoke(ServerMethod[method], message)
            .then(_ => console.log('Sent ' + JSON.stringify(message)))
            .catch(ex => {
            console.log('Error sending ' + JSON.stringify(message));
            console.log(JSON.stringify(ex));
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
//private static eventHubProxy: SignalR.Hub.Proxy;
HubService.reconnectDelay = 5000;
HubService = HubService_1 = __decorate([
    Injectable(),
    __metadata("design:paramtypes", [])
], HubService);
export { HubService };
var HubService_1;
//# sourceMappingURL=hub.service.js.map