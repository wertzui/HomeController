import {Injectable, OnInit} from 'angular2/core';

//export abstract class IHubService {
//    abstract sendMessage(target: string, values: any, methodType: MessageMethod);
//    abstract onReceive(callBack: (message: any) => any);
//}

export enum MessageMethod {
    Get,
    Update
}

enum ServerMethod {
    Send,
    JoinGroup,
    LeaveGroup
}

export class Message {
    Sender: string;
    Target: string;
    Time: Date;
    Values: any;
    Method: string;
}

@Injectable()
export class HubService {
    private static connection: SignalR.Hub.Connection;
    private static eventHubProxy: SignalR.Hub.Proxy;

    constructor() {
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
        this.onReceive(function (message: Message) {
            console.log("Receive " + JSON.stringify(message));
        });
        //super();
    }

    sendMessage(target: string, values: any, methodType: MessageMethod) {
        var message = this.createMessage(target, values, methodType);
        return this.invokeOnServer(ServerMethod.Send, message);
    }

    onReceive(callBack: (message: Message) => any) {
        return HubService.eventHubProxy.on("Receive", callBack);
    }

    private createMessage(target: string, values: any, methodType: MessageMethod): Message{
        return {
            Sender: "WebFrontend",
            Target: target,
            Time: new Date(),
            Values: values,
            Method: MessageMethod[methodType]
        };
    }

    private invokeOnServer(method: ServerMethod, message) {
        return HubService.eventHubProxy.invoke(ServerMethod[method], message)
            .fail(function (ex) {
                console.log("Error sending " + JSON.stringify(message));
                console.log(JSON.stringify(ex));
            })
            .done(function () {
                console.log("Sent " + JSON.stringify(message));
            });
    }

    private joinGroup(groupName: string) {
        return this.invokeOnServer(ServerMethod.JoinGroup, groupName);
    };

    private leaveGroup(groupName) {
        return this.invokeOnServer(ServerMethod.JoinGroup, groupName);
    };
}