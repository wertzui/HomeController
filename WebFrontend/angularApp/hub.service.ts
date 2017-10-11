import { Injectable, OnInit } from '@angular/core';
import { HubConnection } from '@aspnet/signalr-client';

// export abstract class IHubService {
//     abstract sendMessage(target: string, values: any, methodType: MessageMethod);
//     abstract onReceive(callBack: (message: any) => any);
// }

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
    private static connection: HubConnection;
    //private static eventHubProxy: SignalR.Hub.Proxy;
    private static readonly reconnectDelay = 5000;

    constructor() {
        // start the connection
        HubService.connection = new HubConnection('http://localhost:1906/');

        //HubService.eventHubProxy = HubService.connection.createHubProxy('EventHub');

        HubService.connection.onclose(() => {
            console.log('SignalR disconnected, trying to reconnect.')
            setTimeout(() => {
                this.startConnection();
            }, HubService.reconnectDelay);
        });

        // log incoming messages
        // as incoming messages are async, we cannot do it the same way as send, but have to register to the onReceive event.
        this.onReceive(function (message: Message) {
            console.log('Receive ' + JSON.stringify(message));
        });

        this.startConnection();
    }

    private startConnection() {
        const hub = this;
        HubService.connection.start()
            .then(function () {
                console.log('Now connected');
                hub.joinGroup('WebFrontend')
                    .then(_ => hub.sendMessage('FixtureRegister', null, MessageMethod.Get));
            })
            .catch(function () {
                console.log('Could not connect');
            });
    }

    sendMessage(target: string, values: any, methodType: MessageMethod) {
        const message = this.createMessage(target, values, methodType);
        return this.invokeOnServer(ServerMethod.Send, message);
    }

    onReceive(callBack: (message: Message) => any) {
        return HubService.connection.on('Receive', callBack);
    }

    private createMessage(target: string, values: any, methodType: MessageMethod): Message {
        return {
            Sender: 'WebFrontend',
            Target: target,
            Time: new Date(),
            Values: values,
            Method: MessageMethod[methodType]
        };
    }

    private invokeOnServer(method: ServerMethod, message: any) {
        return HubService.connection.invoke(ServerMethod[method], message)
            .then(_ => console.log('Sent ' + JSON.stringify(message)))
            .catch(ex => {
                console.log('Error sending ' + JSON.stringify(message));
                console.log(JSON.stringify(ex));
            });
    }

    private joinGroup(groupName: string) {
        return this.invokeOnServer(ServerMethod.JoinGroup, groupName);
    };

    private leaveGroup(groupName: string) {
        return this.invokeOnServer(ServerMethod.JoinGroup, groupName);
    };
}