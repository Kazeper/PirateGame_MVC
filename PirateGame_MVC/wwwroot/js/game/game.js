"use strict";

var connection = new signalR.HubConnectionBuilder().withUrl("/Game/Index").build();

connection.start().then(function () {
	connection.invoke("AddToGroup", roomId).catch(function (err) {
		return console.error(err.toString());
	})
	connection.invoke("SaveConnectionId", playerNickname).catch(function (err) {
		return console.error(err.toString());
	})
	connection.invoke("StartGame", roomId).catch(function (err) {
		return console.error(err.toString());
	})
}).catch(function (err) {
	return console.error(err.toString());
})

document.getElementById("sendTargetNickname").addEventListener("click", function () {
	connection.invoke("ReceiveTarget", targetNickname).catch(function (err) {
		return console.error(err.toString());
	})
})
connection.on("AskForTarget", function (playerField) {
	console.log("zapytano o CEL ATAKU" + " pole: " + playerField);
})
connection.on("ReceiveNotification", function (message) {
	console.log(message);
})