"use strict";

var connection = new signalR.HubConnectionBuilder().withUrl("/Game").build();

connection.start().then(function () {
	connection.invoke("AddToGroup", roomId).catch(function (err) {
		return console.error(err.toString());
	})
}).catch(function (err) {
	return console.error(err.toString());
})