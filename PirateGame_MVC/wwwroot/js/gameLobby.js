"use strict";
window.addEventListener("load", function (e) {
	var connection = new signalR.HubConnectionBuilder().withUrl("/Lobby/Index").build();
	//Disable send button until connection is established
	document.getElementById("sendButton").disabled = true;

	connection.start().then(function () {
		document.getElementById("sendButton").disabled = false;
	}).catch(function (err) {
		return console.error(err.toString());
	});

	connection.invoke("setPlayerId").catch(function (err) {
		return console.error(err.toString());
	});
});

connection.on("ReceiveMessage", function (user, message) {
	var msg = message.replace(/&/g, "&amp;").replace(/</g, "&lt;").replace(/>/g, "&gt;");
	var encodedMsg = user + " says " + msg;
	var li = document.createElement("li");
	li.textContent = encodedMsg;
	document.getElementById("messagesList").appendChild(li);
});

document.getElementById("sendButton").addEventListener("click", function (event) {
	var user = document.getElementById("playerNickname").value;
	var message = document.getElementById("messageInput").value;
	connection.invoke("SendMessage", user, message).catch(function (err) {
		return console.error(err.toString());
	});
	event.preventDefault();
});