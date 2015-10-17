'use strict';

// SignalR initialization
var hub = (function () {
    // public interface
    var hub = {
        sendMessage: function (target, values, methodType) {
            var message = createMessage(target, values, methodType);
            return invokeOnServer("Send", message);
        },
        onReceive: function (callBack) {
            eventHubProxy.on("Receive", callBack);
        }
    };
    // helper functions
    var createMessage = function (target, values, methodType) {
        return {
            Sender: "WebFrontend",
            Target: target,
            Time: new Date(),
            Values: values,
            Method: methodType
        };
    };
    var invokeOnServer = function (method, message) {
        return eventHubProxy.invoke(method, message)
            .fail(function (ex) {
                console.log("Error sending " + JSON.stringify(message));
                console.log(JSON.stringify(ex));
            })
            .done(function () {
                console.log("Sent " + JSON.stringify(message));
            });
    }
    var joinGroup = function (groupName) {
        return invokeOnServer("JoinGroup", groupName);
    };
    var leaveGroup = function (groupName) {
        return invokeOnServer("LeaveGroup", groupName);
    };

    // start the connection
    var connection = $.hubConnection("http://192.168.0.195:1906/signalr");
    var eventHubProxy = connection.createHubProxy("EventHub");
    connection.start()
        .done(function () {
            console.log('Now connected, connection ID=' + connection.id);
            joinGroup("WebFrontend").done(function () {
                hub.sendMessage("FixtureRegister", null, "Get");
            });
        })
        .fail(function () {
            console.log('Could not connect');
        });

    // log incoming messages
    // as incoming messages are async, we cannot do it the same way as send, but have to register to the onReceive event.
    hub.onReceive(function (message) {
        console.log("Receive " + JSON.stringify(message));
    });

    return hub;
})();



// Angular initilization
(function () {
    angular.module('home', [])
    //.config(function ($stateProvider, $urlRouterProvider) {
    //    $stateProvider
    //        .state('main', {
    //            url: '/',
    //            controller: 'MainController'
    //        });
    //})
    .run(function ($rootScope) {
    });

    angular.module('home')
        .controller('MainController', function ($scope) {
            $scope.Values = {};
            hub.onReceive(function (message) {
                update($scope.Values, message.Values);

                //$scope.Values.rooms = message.Values.rooms;
                $scope.$apply();
            });
            var update = function (oldObj, newObj) {
                var oldPropertyNames = Object.getOwnPropertyNames(oldObj);
                var newPropertyNames = Object.getOwnPropertyNames(newObj);

                // remove properties that are not present in newObj
                for (var oldPropNameIndex in oldPropertyNames) {
                    var oldPropName = oldPropertyNames[oldPropNameIndex];
                    if (!newObj[oldPropName]) {
                        delete oldObj[oldPropName];
                    }
                }

                // update and add all remaining properties
                for (var newPropNameIndex in newPropertyNames) {
                    var newPropName = newPropertyNames[newPropNameIndex];

                    var oldValue = oldObj[newPropName];
                    var newValue = newObj[newPropName];

                    if (oldValue !== newValue) {
                        // value has changed => update
                        var oldType = typeof oldValue;
                        var newType = typeof newValue;

                        if (oldType === newType && oldType === 'object') {
                            // we have an object or an array => go one level deeper
                            update(oldObj[newPropName], newObj[newPropName]);
                        }
                        else {
                            // types do not match or properties are not array or object => replace value
                            oldObj[newPropName] = newObj[newPropName];
                        }
                    }
                }
            };
            $scope.onValueChange = function (fixture) {
                hub.sendMessage(fixture.Target, [angular.copy(fixture)], "Update");
                hub.sendMessage("FixtureRegister", [angular.copy(fixture)], "Update");
            }
        });
})();